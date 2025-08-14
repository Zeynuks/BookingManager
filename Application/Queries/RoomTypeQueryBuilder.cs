using Application.DTOs.Properties;
using Application.Queries.Interfaces;
using Application.Queries.Specs;
using Domain.Entities;

namespace Application.Queries
{
    public sealed class RoomTypeQueryBuilder : IRoomTypeQueryBuilder
    {
        public IQueryable<RoomType> Build( IQueryable<RoomType> source, RoomTypeSearchQueryDto query )
        {
            int requiredGuests = query.Guests.GetValueOrDefault( 1 );

            if ( query.ArrivalDate is not null && query.DepartureDate is not null )
            {
                DateOnly arrivalDate = query.ArrivalDate.Value;
                DateOnly departureDate = query.DepartureDate.Value;
                source = source.Where( RoomTypeSpecs.HasAvailabilityOn( arrivalDate, departureDate, requiredGuests ) );
            }
            else
            {
                source = source.Where( RoomTypeSpecs.HasAvailabilityToday( requiredGuests ) );
            }

            if ( !string.IsNullOrWhiteSpace( query.City ) )
            {
                string cityEquals = query.City!;
                source = source.Where( roomType => roomType.Property.City == cityEquals );
            }

            if ( query.Guests.HasValue )
            {
                source = source.Where( RoomTypeSpecs.CapacityAtLeast( query.Guests.Value ) );
            }

            source = source.Where( RoomTypeSpecs.DailyPriceBetween( query.PriceMin, query.PriceMax ) );

            if ( query.AmenityIds is { Length: > 0 } )
            {
                source = source.Where( RoomTypeSpecs.HasAllAmenities( query.AmenityIds ) );
            }

            if ( query.ServiceIds is { Length: > 0 } )
            {
                source = source.Where( RoomTypeSpecs.HasAllServices( query.ServiceIds ) );
            }

            return RoomTypeOrdering.OrderForSearch( source, query.SortBy, query.Desc );
        }
    }
}