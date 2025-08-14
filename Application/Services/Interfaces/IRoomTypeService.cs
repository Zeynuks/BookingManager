using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomTypeReadDto> Get( int id, CancellationToken ct );
        Task<IReadOnlyList<RoomTypeReadDto>> ListByProperty( int propertyId, CancellationToken ct );
        Task<int> Create( int propertyId, RoomTypeCreateDto dto, CancellationToken ct );
        Task Update( int id, RoomTypeUpdateDto dto, CancellationToken ct );
        Task Remove( int id, CancellationToken ct );
    }
}