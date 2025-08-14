using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<AmenityReadDto> Get( int id, CancellationToken ct );
        Task<IReadOnlyList<AmenityReadDto>> List( CancellationToken ct );
        Task<IReadOnlyList<AmenityReadDto>> ListByRoomType( int roomTypeId, CancellationToken ct );
        Task<int> Create( AmenityCreateDto dto, CancellationToken ct );
        Task Update( int id, AmenityUpdateDto dto, CancellationToken ct );
        Task Remove( int id, CancellationToken ct );
    }
}