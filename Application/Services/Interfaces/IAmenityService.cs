using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<AmenityReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<AmenityReadDto>> GetList( CancellationToken cancellationToken );
        Task<int> Create( AmenityCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, AmenityUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}