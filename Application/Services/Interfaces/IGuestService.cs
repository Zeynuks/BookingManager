using Application.DTOs.Reservations;

namespace Application.Services.Interfaces
{
    public interface IGuestService
    {
        Task<GuestReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<GuestReadDto>> GetList( CancellationToken cancellationToken );
        Task<int> Create( GuestCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, GuestUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}