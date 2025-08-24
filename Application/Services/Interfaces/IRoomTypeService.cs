using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomTypeReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<RoomTypeReadDto>> GetListByProperty( int propertyId, CancellationToken cancellationToken );
        Task<int> Create( int propertyId, RoomTypeCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, RoomTypeUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}