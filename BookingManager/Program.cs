using Application.Queries;
using Application.Queries.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using BookingManager.Middlewares;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Foundation;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BookingManager
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "properties", new OpenApiInfo
                {
                    Title = "Properties API"
                } );
                c.SwaggerDoc( "reservations", new OpenApiInfo
                {
                    Title = "Reservations API"
                } );

                c.DocInclusionPredicate( ( doc, api ) =>
                    string.Equals( api.GroupName, doc, StringComparison.OrdinalIgnoreCase ) );
            } );
            builder.Services.AddControllers();

            bool useInMemory = builder.Configuration.GetValue<bool>( "UseInMemoryDatabase" )
                               || builder.Environment.IsEnvironment( "Testing" );
            string? connectionString = builder.Configuration.GetConnectionString( "Default" );

            if ( string.IsNullOrWhiteSpace( connectionString ) )
            {
                string? server = Environment.GetEnvironmentVariable( "SQL_SERVER" );
                string? database = Environment.GetEnvironmentVariable( "SQL_DATABASE" );
                string? user = Environment.GetEnvironmentVariable( "SQL_USER" );
                string? password = Environment.GetEnvironmentVariable( "SQL_PASSWORD" );

                if ( !string.IsNullOrWhiteSpace( server )
                     && !string.IsNullOrWhiteSpace( database )
                     && !string.IsNullOrWhiteSpace( user )
                     && !string.IsNullOrWhiteSpace( password ) )
                {
                    connectionString = $"Server={server};Database={database};User Id={user};" +
                                       $"Password={password};Encrypt=False;TrustServerCertificate=True";
                    Console.WriteLine( connectionString );
                }
            }

            if ( string.IsNullOrWhiteSpace( connectionString ) && !useInMemory )
            {
                useInMemory = true;
                Console.WriteLine( "No DB connection string found. Falling back to InMemory." );
            }

            builder.Services.AddDbContext<BookingManagerDbContext>( options =>
            {
                if ( useInMemory )
                {
                    options.UseLazyLoadingProxies().UseInMemoryDatabase( "BookingManagerDb" );
                }
                else
                {
                    options.UseLazyLoadingProxies().UseSqlServer( connectionString, sql =>
                    {
                        sql.EnableRetryOnFailure( 5, TimeSpan.FromSeconds( 5 ), null );
                    } );
                }
            } );

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.Decorate<IUnitOfWork, UnitOfWorkExceptionDecorator>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IAmenityService, AmenityService>();
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IRoomTypeSearchService, RoomTypeSearchService>();

            builder.Services.AddScoped<IReservationQueryBuilder, ReservationQueryBuilder>();
            builder.Services.AddScoped<IRoomTypeQueryBuilder, RoomTypeQueryBuilder>();

            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

            WebApplication app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwagger();
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint( "/swagger/properties/swagger.json", "Properties API" );
                    c.SwaggerEndpoint( "/swagger/reservations/swagger.json", "Reservations API" );
                } );
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            using ( IServiceScope scope = app.Services.CreateScope() )
            {
                BookingManagerDbContext db = scope.ServiceProvider.GetRequiredService<BookingManagerDbContext>();

                if ( useInMemory )
                {
                    db.Database.EnsureCreated();
                }
                else
                {
                    db.Database.Migrate();
                }
            }

            app.Run();
        }
    }
}