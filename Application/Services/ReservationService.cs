using Application.DTOs;
using Application.DTOs.Reservations;
using Application.Queries.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationQueryBuilder _reservationQueryBuilder;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(
            IReservationRepository reservationRepository,
            IRoomRepository roomRepository,
            IReservationQueryBuilder reservationQueryBuilder,
            IUnitOfWork unitOfWork )
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _reservationQueryBuilder = reservationQueryBuilder;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResultDto<ReservationReadDto>> GetList(
            ReservationSearchQueryDto query,
            CancellationToken ct )
        {
            IQueryable<Reservation> queryData = _reservationQueryBuilder
                .Build( _reservationRepository.Query().AsNoTracking(), query );

            int total = await queryData.CountAsync( ct );
            int page = Math.Max( 1, query.Page );
            int size = Math.Clamp( query.Size, 1, 200 );

            List<ReservationReadDto> reservations = await queryData
                .Skip( ( int )( ( ( long )page - 1 ) * size ) )
                .Take( size )
                .Select( r => new ReservationReadDto(
                    r.Id,
                    r.RoomId,
                    r.GuestId,
                    r.GuestsCount,
                    r.ArrivalDate,
                    r.DepartureDate,
                    r.ArrivalTime,
                    r.DepartureTime,
                    r.Total,
                    r.Currency.ToString()
                ) )
                .ToListAsync( ct );

            return new PagedResultDto<ReservationReadDto>
            {
                Items = reservations,
                Total = total,
                Page = page,
                Size = size
            };
        }

        public async Task<ReservationReadDto> Get( int id, CancellationToken ct )
        {
            Reservation? r = await _reservationRepository.Get( id, ct );
            if ( r is null )
            {
                throw new DomainNotFoundException( $"Reservation with ID {id} could not be found." );
            }

            return new ReservationReadDto(
                r.Id,
                r.RoomId,
                r.GuestId,
                r.GuestsCount,
                r.ArrivalDate,
                r.DepartureDate,
                r.ArrivalTime,
                r.DepartureTime,
                r.Total,
                r.Currency.ToString() );
        }

        public async Task<int> Create( ReservationCreateDto dto, CancellationToken ct )
        {
            if ( !Enum.TryParse( dto.Currency, true, out Currency currency ) )
            {
                throw new BusinessRuleViolationException( $"Invalid currency value: {dto.Currency}" );
            }

            Room? room = await _roomRepository.Query()
                .Include( r => r.RoomType )
                .FirstOrDefaultAsync( r => r.Id == dto.RoomId, ct );

            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {dto.RoomId} could not be found." );
            }

            await EnsureCapacityOrThrow(
                room.Id,
                dto.GuestsCount,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                excludeReservationId: null,
                ct );

            decimal total = CalculateTotal(
                room.RoomType,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                dto.GuestsCount );

            Reservation reservation = new(
                dto.RoomId,
                dto.GuestId,
                dto.GuestsCount,
                dto.ArrivalDate,
                dto.DepartureDate,
                dto.ArrivalTime,
                dto.DepartureTime,
                total,
                currency
            );

            _reservationRepository.Add( reservation );
            await _unitOfWork.CommitAsync( ct );

            return reservation.Id;
        }

        public async Task Update( int id, ReservationUpdateDto dto, CancellationToken ct )
        {
            Reservation? current = await _reservationRepository.Query()
                .Include( r => r.Room )
                .ThenInclude( rm => rm.RoomType )
                .FirstOrDefaultAsync( r => r.Id == id, ct );

            if ( current is null )
            {
                throw new DomainNotFoundException( $"Reservation with ID {id} could not be found." );
            }

            if ( !Enum.TryParse( dto.Currency, true, out Currency currency ) )
            {
                throw new BusinessRuleViolationException( $"Invalid currency value: {dto.Currency}" );
            }

            Room? room = await _roomRepository.Query()
                .Include( r => r.RoomType )
                .FirstOrDefaultAsync( r => r.Id == dto.RoomId, ct );

            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {dto.RoomId} could not be found." );
            }

            await EnsureCapacityOrThrow(
                room.Id,
                dto.GuestsCount,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                excludeReservationId: id,
                ct );

            decimal total = CalculateTotal(
                room.RoomType,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                dto.GuestsCount );

            current.Update(
                dto.GuestsCount,
                dto.ArrivalDate,
                dto.DepartureDate,
                dto.ArrivalTime,
                dto.DepartureTime,
                total,
                currency
            );

            await _unitOfWork.CommitAsync( ct );
        }

        public async Task Remove( int id, CancellationToken ct )
        {
            Reservation? reservation = await _reservationRepository.Get( id, ct );
            if ( reservation is null )
            {
                throw new DomainNotFoundException( $"Reservation with ID {id} could not be found." );
            }

            _reservationRepository.Delete( reservation );
            await _unitOfWork.CommitAsync( ct );
        }

        private async Task EnsureCapacityOrThrow(
            int roomId,
            int guestsCount,
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime,
            int? excludeReservationId,
            CancellationToken ct )
        {
            EnsureChronologyOrThrow( arrivalDate, arrivalTime, departureDate, departureTime );

            Room room = await _roomRepository.Query()
                .Include( r => r.RoomType )
                .Include( r => r.Reservations )
                .FirstAsync( r => r.Id == roomId, ct );

            RoomType rt = room.RoomType;

            DateTime start = arrivalDate.ToDateTime( arrivalTime );
            DateTime end = departureDate.ToDateTime( departureTime );

            IEnumerable<Reservation> overlaps = room.Reservations
                .Where( r => excludeReservationId is null || r.Id != excludeReservationId.Value )
                .Where( r =>
                {
                    DateTime oStart = r.ArrivalDate.ToDateTime( r.ArrivalTime );
                    DateTime oEnd = r.DepartureDate.ToDateTime( r.DepartureTime );
                    return Overlaps( start, end, oStart, oEnd );
                } );

            if ( rt.IsSharedOccupancy )
            {
                int occupied = overlaps.Sum( r => r.GuestsCount );
                int available = rt.MaxPlaces - occupied;

                if ( guestsCount > available )
                {
                    throw new BusinessRuleViolationException(
                        $"Not enough places in shared room. Available: {available}, requested: {guestsCount}." );
                }
            }
            else
            {
                if ( overlaps.Any() )
                {
                    throw new BusinessRuleViolationException( "Room is already reserved for selected period." );
                }

                if ( guestsCount > rt.MaxPlaces )
                {
                    throw new BusinessRuleViolationException(
                        $"Guests count exceeds room capacity {rt.MaxPlaces}." );
                }
            }

            return;

            static bool Overlaps( DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd )
            {
                return aStart < bEnd && bStart < aEnd;
            }
        }


        private static decimal CalculateTotal(
            RoomType roomType,
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime,
            int guestsCount )
        {
            EnsureChronologyOrThrow( arrivalDate, arrivalTime, departureDate, departureTime );

            int days = arrivalDate == departureDate ? 1 : departureDate.DayNumber - arrivalDate.DayNumber;

            decimal baseTotal = roomType.DailyPrice * days;

            if ( roomType.IsSharedOccupancy )
            {
                return baseTotal * guestsCount;
            }

            return baseTotal;
        }

        private static void EnsureChronologyOrThrow(
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime )
        {
            if ( arrivalDate > departureDate )
            {
                throw new BusinessRuleViolationException( "Departure must be after arrival." );
            }

            if ( arrivalDate == departureDate && departureTime <= arrivalTime )
            {
                throw new BusinessRuleViolationException(
                    "When dates are the same, departure time must be after arrival time." );
            }
        }
    }
}