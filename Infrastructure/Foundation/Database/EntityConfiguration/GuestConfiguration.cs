using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration
{
    internal class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure( EntityTypeBuilder<Guest> builder )
        {
            builder.ToTable( nameof( Guest ) )
                .HasKey( g => g.Id );

            builder.Property( g => g.Name )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( g => g.PhoneNumber )
                .HasMaxLength( 15 )
                .IsRequired();

            builder.HasAlternateKey( g => g.PhoneNumber );

            builder.HasMany( g => g.Reservations )
                .WithOne( r => r.Guest )
                .HasForeignKey( r => r.GuestId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}