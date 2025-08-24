using Application.DTOs.Properties;
using Domain.Entities;

namespace Application.Queries
{
    public static class RoomTypeOrdering
    {
        public static IOrderedQueryable<RoomType> OrderForSearch(
            IQueryable<RoomType> source,
            RoomTypeSortBy sortBy,
            bool descending )
        {
            return ( sortBy, descending ) switch
            {
                (RoomTypeSortBy.Price, false) => source.OrderBy( roomType => roomType.DailyPrice )
                    .ThenBy( roomType => roomType.Id ),
                (RoomTypeSortBy.Price, true) => source.OrderByDescending( roomType => roomType.DailyPrice )
                    .ThenByDescending( roomType => roomType.Id ),

                (RoomTypeSortBy.City, false) => source.OrderBy( roomType => roomType.Property.City )
                    .ThenBy( roomType => roomType.Id ),
                (RoomTypeSortBy.City, true) => source.OrderByDescending( roomType => roomType.Property.City )
                    .ThenByDescending( roomType => roomType.Id ),

                (RoomTypeSortBy.MaxPlaces, false) => source.OrderBy( roomType => roomType.MaxPlaces )
                    .ThenBy( roomType => roomType.Id ),
                (RoomTypeSortBy.MaxPlaces, true) => source.OrderByDescending( roomType => roomType.MaxPlaces )
                    .ThenByDescending( roomType => roomType.Id ),

                _ => source.OrderBy( roomType => roomType.Id )
            };
        }
    }
}