using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.Extensions
{
    public sealed class DatabaseRuntimeOptions
    {
        public bool UseInMemory { get; init; }
    }

    public static class DatabaseInitializer
    {
        public static void AddBookingManagerDatabase( this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment )
        {
            bool configuredUseInMemory = configuration.GetValue<bool>( "UseInMemoryDatabase" );
            bool isTesting = environment.IsEnvironment( "Testing" );

            string? fromConfig = configuration.GetConnectionString( "Default" );
            string? resolved = ResolveConnectionString( fromConfig );

            bool finalUseInMemory = configuredUseInMemory || isTesting || string.IsNullOrWhiteSpace( resolved );

            if ( string.IsNullOrWhiteSpace( resolved ) && !finalUseInMemory )
            {
                finalUseInMemory = true;
            }

            services.AddSingleton( new DatabaseRuntimeOptions
            {
                UseInMemory = finalUseInMemory
            } );

            services.AddDbContext<BookingManagerDbContext>( options =>
            {
                if ( finalUseInMemory )
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
            DatabaseRuntimeOptions runtime = scope.ServiceProvider.GetRequiredService<DatabaseRuntimeOptions>();
            BookingManagerDbContext db = scope.ServiceProvider.GetRequiredService<BookingManagerDbContext>();

            if ( runtime.UseInMemory )
            {
                db.Database.EnsureCreated();
            }
            else
            {
                db.Database.Migrate();
            }
        }

        private static string? ResolveConnectionString( string? fromConfig )
        {
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
                return
                    $"Server={server};Database={database};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True";
            }

            return null;
        }
    }
}