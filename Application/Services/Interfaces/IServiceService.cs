using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<ServiceReadDto>> GetList( CancellationToken cancellationToken );
        Task<int> Create( ServiceCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, ServiceUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}