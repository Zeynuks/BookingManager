using Application.Queries;
using Application.Queries.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Repositories;
using Infrastructure.Foundation;
using Infrastructure.Repositories;

namespace BookingManager.Extensions
{
    public static class Bindings
    {
        public static void AddBindings( this IServiceCollection services )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Decorate<IUnitOfWork, UnitOfWorkExceptionDecorator>();

            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRoomTypeSearchService, RoomTypeSearchService>();

            services.AddScoped<IReservationQueryBuilder, ReservationQueryBuilder>();
            services.AddScoped<IRoomTypeQueryBuilder, RoomTypeQueryBuilder>();

            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
        }
    }
}