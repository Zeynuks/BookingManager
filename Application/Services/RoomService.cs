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

        public async Task<RoomReadDto> GetById( int id )
        {
            Room? room = await _roomRepository.TryGet( id );
            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {id} could not be found." );
            }

            return new RoomReadDto(
                room.Id,
                room.RoomTypeId,
                room.Number );
        }

        public async Task<IReadOnlyList<RoomReadDto>> GetListByRoomTypeId( int roomTypeId )
        {
            RoomType? roomType = await _roomTypesRepository.TryGet( roomTypeId );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"Room with ID {roomTypeId} could not be found." );
            }

            IReadOnlyList<Room> rooms = await _roomRepository.GetListByRoomType( roomTypeId );

            return rooms
                .Select( r => new RoomReadDto(
                    r.Id,
                    r.RoomTypeId,
                    r.Number ) )
                .ToList();
        }

        public async Task<int> Create( int roomTypeId, RoomCreateDto dto )
        {
            RoomType? roomType = await _roomTypesRepository.TryGet( roomTypeId );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {roomTypeId} could not be found." );
            }

            Room room = new(
                roomTypeId,
                dto.Number );

            _roomRepository.Add( room );
            await _unitOfWork.CommitAsync();

            return room.Id;
        }

        public async Task Update( int id, RoomUpdateDto dto )
        {
            Room? room = await _roomRepository.TryGet( id );
            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {id} could not be found." );
            }

            RoomType? roomType = await _roomTypesRepository.TryGet( room.RoomTypeId );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {room.RoomTypeId} could not be found." );
            }

            room.Update( dto.RoomTypeId, dto.Number );
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Room? room = await _roomRepository.TryGet( id );
            if ( room is null )
            {
                throw new DomainNotFoundException( $"Room with ID {id} could not be found." );
            }

            _roomRepository.Delete( room );
            await _unitOfWork.CommitAsync();
        }
    }
}