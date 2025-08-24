using Application.DTOs.Properties;
using Domain.Entities;

namespace Application.Queries.Interfaces
{
    public interface IRoomTypeQueryBuilder
    {
        IQueryable<RoomType> Build( RoomTypeSearchQueryDto query );
    }
}