using Application.DTOs;
using Application.DTOs.Reservations;
using Application.Queries.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

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

        public async Task<PagedResultDto<ReservationReadDto>> GetByPage(
            ReservationSearchQueryDto query,
            CancellationToken cancellationToken )
        {
            IQueryable<Reservation> queryData = _reservationQueryBuilder.Build( query );

            int total = await _reservationRepository.Count( queryData, cancellationToken );
            int page = Math.Max( 1, query.Page );
            int size = Math.Clamp( query.Size, 1, 200 );

            IReadOnlyList<Reservation> entities = await _reservationRepository.GetPage(
                queryData,
                page,
                size,
                cancellationToken );

            IReadOnlyList<ReservationReadDto> items = entities
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
                .ToList();

            return new PagedResultDto<ReservationReadDto>
            {
                Items = items,
                Total = total,
                Page = page,
                Size = size
            };
        }

        public async Task<ReservationReadDto> GetById( int id, CancellationToken cancellationToken )
        {
            Reservation? r = await _reservationRepository.TryGet( id, cancellationToken );
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

        public async Task<int> Create( ReservationCreateDto dto, CancellationToken cancellationToken )
        {
            if ( !Enum.TryParse( dto.Currency, true, out Currency currency ) )
            {
                throw new BusinessRuleViolationException( $"Invalid currency value: {dto.Currency}" );
            }

            Room? room = await _roomRepository.TryGet( dto.RoomId, cancellationToken );

            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {dto.RoomId} could not be found." );
            }

            bool isAvailable = await _roomRepository.IsAvailable(
                room.Id,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                dto.GuestsCount,
                cancellationToken );

            if ( !isAvailable )
            {
                throw new DomainNotFoundException( $"Room is not available" );
            }

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
            await _unitOfWork.CommitAsync( cancellationToken );

            return reservation.Id;
        }

        public async Task Update( int id, ReservationUpdateDto dto, CancellationToken cancellationToken )
        {
            Reservation? reservation = await _reservationRepository.TryGet( id, cancellationToken );

            if ( reservation is null )
            {
                throw new DomainNotFoundException( $"Reservation with ID {id} could not be found." );
            }

            if ( !Enum.TryParse( dto.Currency, true, out Currency currency ) )
            {
                throw new BusinessRuleViolationException( $"Invalid currency value: {dto.Currency}" );
            }

            Room? room = await _roomRepository.TryGet( dto.RoomId, cancellationToken );

            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {dto.RoomId} could not be found." );
            }

            bool isAvailable = await _roomRepository.IsAvailable(
                room.Id,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                dto.GuestsCount,
                cancellationToken );

            if ( !isAvailable )
            {
                throw new DomainNotFoundException( $"Room is not available" );
            }

            decimal total = CalculateTotal(
                room.RoomType,
                dto.ArrivalDate,
                dto.ArrivalTime,
                dto.DepartureDate,
                dto.DepartureTime,
                dto.GuestsCount );

            reservation.Update(
                dto.GuestsCount,
                dto.ArrivalDate,
                dto.DepartureDate,
                dto.ArrivalTime,
                dto.DepartureTime,
                total,
                currency
            );

            await _unitOfWork.CommitAsync( cancellationToken );
        }

        public async Task Remove( int id, CancellationToken cancellationToken )
        {
            Reservation? reservation = await _reservationRepository.TryGet( id, cancellationToken );
            if ( reservation is null )
            {
                throw new DomainNotFoundException( $"Reservation with ID {id} could not be found." );
            }

            _reservationRepository.Delete( reservation );
            await _unitOfWork.CommitAsync( cancellationToken );
        }

        private static decimal CalculateTotal(
            RoomType roomType,
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime,
            int guestsCount )
        {
            ValidateDate( arrivalDate, arrivalTime, departureDate, departureTime );

            int days = arrivalDate == departureDate ? 1 : departureDate.DayNumber - arrivalDate.DayNumber;

            decimal baseTotal = roomType.DailyPrice * days;

            if ( roomType.IsSharedOccupancy )
            {
                return baseTotal * guestsCount;
            }

            return baseTotal;
        }

        private static void ValidateDate(
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