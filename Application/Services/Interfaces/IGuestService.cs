using Application.DTOs.Reservations;

namespace Application.Services.Interfaces
{
    public interface IGuestService
    {
        Task<GuestReadDto> GetById( int id );
        Task<IReadOnlyList<GuestReadDto>> GetList();
        Task<int> Create( GuestCreateDto dto );
        Task Update( int id, GuestUpdateDto dto );
        Task Remove( int id );
    }
}