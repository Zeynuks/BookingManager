using Domain.Entity;
using Domain.Repository;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingManager
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            builder.Services.AddDbContext<BookingManagerDbContext>( options =>
            {
                options.LogTo( Console.WriteLine );
            } );

            builder.Services.AddScoped<IRepository<Amenity>, AmenityRepository>();
            builder.Services.AddScoped<IRepository<Guest>, GuestRepository>();
            builder.Services.AddScoped<IRepository<Property>, PropertyRepository>();
            builder.Services.AddScoped<IRepository<Reservation>, ReservationRepository>();
            builder.Services.AddScoped<IRepository<RoomType>, RoomTypeRepository>();
            builder.Services.AddScoped<IRepository<Service>, ServiceRepository>();

            WebApplication app = builder.Build();

            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.UseHttpsRedirection();
            
            using (IServiceScope scope = app.Services.CreateScope())
            {
                BookingManagerDbContext dbContext = scope.ServiceProvider.GetRequiredService<BookingManagerDbContext>();
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}