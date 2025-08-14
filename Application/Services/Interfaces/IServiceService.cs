using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceReadDto> Get( int id, CancellationToken ct );
        Task<IReadOnlyList<ServiceReadDto>> List( CancellationToken ct );
        Task<IReadOnlyList<ServiceReadDto>> ListByRoomType( int roomTypeId, CancellationToken ct );
        Task<int> Create( ServiceCreateDto dto, CancellationToken ct );
        Task Update( int id, ServiceUpdateDto dto, CancellationToken ct );
        Task Remove( int id, CancellationToken ct );
    }
}