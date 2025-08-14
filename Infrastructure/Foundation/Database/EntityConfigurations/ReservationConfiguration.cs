using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure( EntityTypeBuilder<Reservation> builder )
        {
            builder.ToTable( nameof( Reservation ) )
                .HasKey( r => r.Id );

            builder.Property( r => r.RoomId )
                .IsRequired();

            builder.Property( r => r.GuestId )
                .IsRequired();

            builder.Property( r => r.ArrivalDate )
                .HasColumnType( "date" )
                .IsRequired();

            builder.Property( r => r.DepartureDate )
                .HasColumnType( "date" )
                .IsRequired();

            builder.Property( r => r.ArrivalTime )
                .HasColumnType( "time" )
                .IsRequired();

            builder.Property( r => r.DepartureTime )
                .HasColumnType( "time" )
                .IsRequired();

            builder.Property( r => r.Total )
                .HasPrecision( 18, 2 )
                .IsRequired();

            builder.Property( p => p.Currency )
                .IsRequired();

            builder.HasIndex( r => r.RoomId );
            builder.HasIndex( r => r.GuestId );
            builder.HasIndex( r => new
            {
                r.RoomId,
                r.ArrivalDate,
                r.DepartureDate
            } );

            builder.HasOne( r => r.Room )
                .WithMany()
                .HasForeignKey( r => r.RoomId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasOne( r => r.Guest )
                .WithMany( g => g.Reservations )
                .HasForeignKey( r => r.GuestId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}