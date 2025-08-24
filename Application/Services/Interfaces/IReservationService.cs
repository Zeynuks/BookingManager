using Application.DTOs;
using Application.DTOs.Reservations;

namespace Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<PagedResultDto<ReservationReadDto>> GetByPage(
            ReservationSearchQueryDto query,
            CancellationToken cancellationToken );

        Task<ReservationReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<int> Create( ReservationCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, ReservationUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}