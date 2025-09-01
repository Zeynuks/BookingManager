using Application.DTOs;
using Application.DTOs.Reservations;

namespace Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<PagedResultDto<ReservationReadDto>> GetByPage( ReservationSearchQueryDto query );

        Task<ReservationReadDto> GetById( int id );
        Task<int> Create( ReservationCreateDto dto );
        Task Update( int id, ReservationUpdateDto dto );
        Task Remove( int id );
    }
}