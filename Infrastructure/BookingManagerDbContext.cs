using Domain.Entities;
using Infrastructure.Foundation.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookingManagerDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        
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