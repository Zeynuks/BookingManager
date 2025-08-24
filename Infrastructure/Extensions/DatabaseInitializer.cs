using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DatabaseInitializer
    {
        public static void AddBookingManagerDatabase(
            this IServiceCollection services,
            IConfiguration configuration )
        {
            string? resolved = ResolveConnectionString( configuration );

            services.AddDbContext<BookingManagerDbContext>( options =>
            {
                if ( string.IsNullOrWhiteSpace( resolved ) )
                {
                    options.UseLazyLoadingProxies().UseInMemoryDatabase( "BookingManagerDb" );
                }
                else
                {
                    options.UseLazyLoadingProxies().UseSqlServer( resolved, sql =>
                    {
                        sql.EnableRetryOnFailure( 5, TimeSpan.FromSeconds( 5 ), null );
                    } );
                }
            } );
        }

        public static void InitBookingManagerDatabase( this WebApplication app )
        {
            using IServiceScope scope = app.Services.CreateScope();
            BookingManagerDbContext db = scope.ServiceProvider.GetRequiredService<BookingManagerDbContext>();

            bool isInMemory = db.Database.ProviderName != null
                              && db.Database.ProviderName.Contains( "InMemory", StringComparison.OrdinalIgnoreCase );

            if ( isInMemory )
            {
                db.Database.EnsureCreated();
            }
            else if ( db.Database.CanConnect() )
            {
                db.Database.Migrate();
            }
            else
            {
                throw new InvalidOperationException( "Can't connect to database." );
            }
        }

        private static string? ResolveConnectionString( IConfiguration configuration )
        {
            string? fromConfig = configuration.GetConnectionString( "Default" );
            if ( !string.IsNullOrWhiteSpace( fromConfig ) )
            {
                return fromConfig;
            }

            string? server = Environment.GetEnvironmentVariable( "SQL_SERVER" );
            string? database = Environment.GetEnvironmentVariable( "SQL_DATABASE" );
            string? user = Environment.GetEnvironmentVariable( "SQL_USER" );
            string? password = Environment.GetEnvironmentVariable( "SQL_PASSWORD" );

            if ( !string.IsNullOrWhiteSpace( server )
                 && !string.IsNullOrWhiteSpace( database )
                 && !string.IsNullOrWhiteSpace( user )
                 && !string.IsNullOrWhiteSpace( password ) )
            {
                return $"Server={server};Database={database};User Id={user};" +
                       $"Password={password};Encrypt=False;TrustServerCertificate=True";
            }

            return null;
        }
    }
}