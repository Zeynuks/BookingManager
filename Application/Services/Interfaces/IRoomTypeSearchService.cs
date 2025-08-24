using Application.DTOs;
using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomTypeSearchService
    {
        Task<PagedResultDto<RoomTypeReadDto>> GetByPage( RoomTypeSearchQueryDto query,
            CancellationToken cancellationToken );
    }
}