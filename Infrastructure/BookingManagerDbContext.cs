using Infrastructure.Foundation.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookingManagerDbContext : DbContext
    {
        public BookingManagerDbContext( DbContextOptions<BookingManagerDbContext> options )
            : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
            modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
            modelBuilder.ApplyConfiguration( new RoomConfiguration() );
            modelBuilder.ApplyConfiguration( new ServiceConfiguration() );
            modelBuilder.ApplyConfiguration( new AmenityConfiguration() );
            modelBuilder.ApplyConfiguration( new GuestConfiguration() );
            modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        }
    }
}