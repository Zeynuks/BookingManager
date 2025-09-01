using Application.DTOs;
using Application.DTOs.Properties;
using Application.Queries.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;

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

        public async Task<PagedResultDto<RoomTypeReadDto>> GetByPage(
            RoomTypeSearchQueryDto query )
        {
            IQueryable<RoomType> queryData = _roomTypeQueryBuilder.Build( query );

            int total = await _roomTypeRepository.Count( queryData );
            int page = Math.Max( 1, query.Page );
            int size = Math.Clamp( query.Size, 1, 200 );

            IReadOnlyList<RoomType> entities = await _roomTypeRepository.GetPage(
                queryData,
                page,
                size );

            IReadOnlyList<RoomTypeReadDto> items = entities
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
                .ToList();

            return new PagedResultDto<RoomTypeReadDto>
            {
                Items = items,
                Total = total,
                Page = page,
                Size = size
            };
        }
    }
}