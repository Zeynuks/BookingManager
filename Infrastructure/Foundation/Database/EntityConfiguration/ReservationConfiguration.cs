using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure( EntityTypeBuilder<Reservation> builder )
        {
            builder.ToTable( nameof( Reservation ) )
                .HasKey( r => r.Id );

            builder.Property( r => r.ArrivalDate )
                .IsRequired();

            builder.Property( r => r.DepartureDate )
                .IsRequired();

            builder.Property( r => r.ArrivalTime )
                .IsRequired();

            builder.Property( r => r.DepartureTime )
                .IsRequired();

            builder.Property( r => r.Total )
                .HasPrecision( 18, 2 )
                .IsRequired();

            builder.HasIndex( r => r.RoomTypeId );

            builder.HasIndex( r => r.GuestId );

            builder.HasOne( r => r.RoomType )
                .WithMany()
                .HasForeignKey( r => r.RoomTypeId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasOne( r => r.Guest )
                .WithMany( g => g.Reservations )
                .HasForeignKey( r => r.GuestId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}