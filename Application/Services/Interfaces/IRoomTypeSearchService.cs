using Application.DTOs;
using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomTypeSearchService
    {
        Task<PagedResultDto<RoomTypeReadDto>> GetList( RoomTypeSearchQueryDto query, CancellationToken ct );
    }
}