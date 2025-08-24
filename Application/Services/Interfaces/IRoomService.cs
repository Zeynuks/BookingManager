using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<RoomReadDto>> GetListByRoomTypeId( int roomTypeId, CancellationToken cancellationToken );
        Task<int> Create( int propertyId, RoomCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, RoomUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}