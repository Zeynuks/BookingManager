using Application.DTOs.Reservations;

namespace Application.Services.Interfaces
{
    public interface IGuestService
    {
        Task<GuestReadDto> Get( int id, CancellationToken ct );
        Task<IReadOnlyList<GuestReadDto>> GetList( CancellationToken ct );
        Task<int> Create( GuestCreateDto dto, CancellationToken ct );
        Task Update( int id, GuestUpdateDto dto, CancellationToken ct );
        Task Remove( int id, CancellationToken ct );
    }
}