using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomReadDto> Get( int id, CancellationToken ct );
        Task<IReadOnlyList<RoomReadDto>> GetListByRoomTypeId( int roomTypeId, CancellationToken ct );
        Task<int> Create( int propertyId, RoomCreateDto dto, CancellationToken ct );
        Task Update( int id, RoomUpdateDto dto, CancellationToken ct );
        Task Remove( int id, CancellationToken ct );
    }
}