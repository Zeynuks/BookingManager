using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure( EntityTypeBuilder<Guest> builder )
        {
            builder.ToTable( nameof( Guest ) )
                .HasKey( g => g.Id );

            builder.Property( g => g.Name )
                .HasMaxLength( 200 )
                .IsRequired();

            builder.Property( g => g.PhoneNumber )
                .HasMaxLength( 30 )
                .IsRequired();

            builder.HasIndex( g => g.PhoneNumber )
                .IsUnique();

            builder.HasMany( g => g.Reservations )
                .WithOne( r => r.Guest )
                .HasForeignKey( r => r.GuestId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Navigation( g => g.Reservations )
                .UsePropertyAccessMode( PropertyAccessMode.Field );
            builder.Metadata.FindNavigation( nameof( Guest.Reservations ) )!
                .SetField( "_reservations" );
        }
    }
}