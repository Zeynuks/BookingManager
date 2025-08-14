using Application.DTOs;
using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomTypeSearchService
    {
        Task<PagedResultDto<RoomTypeReadDto>> List( RoomTypeSearchQueryDto query, CancellationToken ct );
    }
}