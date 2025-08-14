using Application.DTOs;
using Application.DTOs.Properties;
using Application.Queries.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class RoomTypeSearchService : IRoomTypeSearchService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomTypeQueryBuilder _roomTypeQueryBuilder;

        public RoomTypeSearchService(
            IRoomTypeRepository roomTypeRepository,
            IRoomTypeQueryBuilder roomTypeQueryBuilder )
        {
            _roomTypeRepository = roomTypeRepository;
            _roomTypeQueryBuilder = roomTypeQueryBuilder;
        }

        public async Task<PagedResultDto<RoomTypeReadDto>> List(
            RoomTypeSearchQueryDto query,
            CancellationToken ct )
        {
            IQueryable<RoomType> queryData = _roomTypeQueryBuilder
                .Build( _roomTypeRepository.Query().AsNoTracking(), query );

            int total = await queryData.CountAsync( ct );

            int page = Math.Max( 1, query.Page );
            int size = Math.Clamp( query.Size, 1, 200 );

            List<RoomTypeReadDto> roomTypes = await queryData
                .Skip( ( int )( ( ( long )page - 1 ) * size ) )
                .Take( size )
                .Select( rt => new RoomTypeReadDto(
                    rt.Id,
                    rt.PropertyId,
                    rt.Name,
                    rt.DailyPrice,
                    rt.MaxPlaces,
                    rt.IsSharedOccupancy,
                    rt.Services.Select( s => new ServiceReadDto( s.Id, s.Name ) ).ToList(),
                    rt.Amenities.Select( a => new AmenityReadDto( a.Id, a.Name ) ).ToList()
                ) )
                .ToListAsync( ct );

            return new PagedResultDto<RoomTypeReadDto>
            {
                Items = roomTypes,
                Total = total,
                Page = page,
                Size = size
            };
        }
    }
}