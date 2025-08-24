using Application.DTOs.Reservations;
using Application.Queries.Interfaces;
using Application.Queries.Specs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Queries
{
    public class ReservationQueryBuilder : IReservationQueryBuilder
    {
        private readonly IReservationRepository _reservationRepository;

        private ReservationQueryBuilder( IReservationRepository reservationRepository )
        {
            _reservationRepository = reservationRepository;
        }

        public IQueryable<Reservation> Build( ReservationSearchQueryDto query )
        {
            IQueryable<Reservation> source = _reservationRepository.Query();

            if ( query.GuestId.HasValue )
            {
                source = source.Where( ReservationSpecs.ByGuest( query.GuestId.Value ) );
            }

            if ( query.RoomTypeId.HasValue )
            {
                source = source.Where( ReservationSpecs.ByRoomType( query.RoomTypeId.Value ) );
            }

            if ( query.PropertyId.HasValue )
            {
                source = source.Where( ReservationSpecs.ByProperty( query.PropertyId.Value ) );
            }

            if ( query.From.HasValue )
            {
                source = source.Where( ReservationSpecs.FromDateInclusive( query.From.Value ) );
            }

            if ( query.To.HasValue )
            {
                source = source.Where( ReservationSpecs.ToDateInclusive( query.To.Value ) );
            }

            return ReservationOrdering.OrderForSearch( source, query.SortBy, query.Desc );
        }
    }
}