using Application.DTOs;
using Application.DTOs.Reservations;

namespace Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<PagedResultDto<ReservationReadDto>> GetList(ReservationSearchQueryDto query, CancellationToken ct);
        Task<ReservationReadDto> Get(int id, CancellationToken ct);
        Task<int> Create(ReservationCreateDto dto, CancellationToken ct);
        Task Update(int id, ReservationUpdateDto dto, CancellationToken ct);
        Task Remove(int id, CancellationToken ct);
    }
}