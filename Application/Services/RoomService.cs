using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(
            IRoomRepository roomRepository,
            IRoomTypeRepository roomTypeRepository,
            IUnitOfWork unitOfWork )
        {
            _roomRepository = roomRepository;
            _roomTypesRepository = roomTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomReadDto> Get( int id, CancellationToken ct )
        {
            Room? room = await _roomRepository.Get( id, ct );
            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {id} could not be found." );
            }

            return new RoomReadDto(
                room.Id,
                room.RoomTypeId,
                room.Number );
        }

        public async Task<IReadOnlyList<RoomReadDto>> ListByRoomTypeId( int roomTypeId, CancellationToken ct )
        {
            RoomType? roomType = await _roomTypesRepository.Get( roomTypeId, ct );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"Room with ID {roomTypeId} could not be found." );
            }

            List<Room> rooms = await _roomRepository.ListByRoomType( roomTypeId, ct );

            return rooms
                .Select( r => new RoomReadDto(
                    r.Id,
                    r.RoomTypeId,
                    r.Number ) )
                .ToList();
        }

        public async Task<int> Create( int roomTypeId, RoomCreateDto dto, CancellationToken ct )
        {
            RoomType? roomType = await _roomTypesRepository.Get( roomTypeId, ct );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {roomTypeId} could not be found." );
            }

            Room room = new(
                roomTypeId,
                dto.Number );

            await _unitOfWork.CommitAsync( ct );

            return room.Id;
        }

        public async Task Update( int id, RoomUpdateDto dto, CancellationToken ct )
        {
            Room? room = await _roomRepository.Get( id, ct );
            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {id} could not be found." );
            }

            RoomType? roomType = await _roomTypesRepository.Get( room.RoomTypeId, ct );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {room.RoomTypeId} could not be found." );
            }

            room.Update( dto.RoomTypeId, dto.Number );
            await _unitOfWork.CommitAsync( ct );
        }

        public async Task Remove( int id, CancellationToken ct )
        {
            Room? room = await _roomRepository.Get( id, ct );
            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {id} could not be found." );
            }

            _roomRepository.Delete( room );
            await _unitOfWork.CommitAsync( ct );
        }
    }
}