using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomReadDto> GetById( int id );
        Task<IReadOnlyList<RoomReadDto>> GetListByRoomTypeId( int roomTypeId );
        Task<int> Create( int propertyId, RoomCreateDto dto );
        Task Update( int id, RoomUpdateDto dto );
        Task Remove( int id );
    }
}