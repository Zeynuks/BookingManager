using Infrastructure.Foundation.Database.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookingManagerDbContext : DbContext
    {
        public BookingManagerDbContext( DbContextOptions<BookingManagerDbContext> options )
            : base( options )
        {
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if ( optionsBuilder.IsConfigured )
            {
                return;
            }

            string? server = Environment.GetEnvironmentVariable( "SQL_SERVER" );
            string? database = Environment.GetEnvironmentVariable( "SQL_DATABASE" );
            string? user = Environment.GetEnvironmentVariable( "SQL_USER" );
            string? password = Environment.GetEnvironmentVariable( "SQL_PASSWORD" );

            if ( string.IsNullOrEmpty( server ) ||
                 string.IsNullOrEmpty( database ) ||
                 string.IsNullOrEmpty( user ) ||
                 string.IsNullOrEmpty( password ) )
            {
                throw new InvalidOperationException( "Одна или несколько переменных окружения " +
                                                     "для подключения не установлены." );
            }

            string connectionString = $"Server={server};Database={database};User={user};Password={password};" +
                                      $"Encrypt=False;TrustServerCertificate=True";


            optionsBuilder.UseSqlServer( connectionString );
        }


        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.ApplyConfiguration( new AmenityConfiguration() );
            modelBuilder.ApplyConfiguration( new GuestConfiguration() );
            modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
            modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
            modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
            modelBuilder.ApplyConfiguration( new ServiceConfiguration() );
        }
    }
}