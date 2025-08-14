using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure( EntityTypeBuilder<Property> builder )
        {
            builder.ToTable( nameof( Property ) )
                .HasKey( p => p.Id );

            builder.Property( p => p.Name )
                .HasMaxLength( 200 )
                .IsRequired();

            builder.Property( p => p.Country )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( p => p.City )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( p => p.Address )
                .HasMaxLength( 300 )
                .IsRequired();

            builder.Property( p => p.Latitude )
                .HasColumnType( "decimal(9,6)" )
                .IsRequired();

            builder.Property( p => p.Longitude )
                .HasColumnType( "decimal(9,6)" )
                .IsRequired();

            builder.Property( p => p.Currency )
                .IsRequired(); 

            builder.HasIndex( p => new
                {
                    p.Name,
                    p.Country,
                    p.City,
                    p.Address
                } )
                .IsUnique();

            builder.HasMany( p => p.RoomTypes )
                .WithOne( rt => rt.Property )
                .HasForeignKey( rt => rt.PropertyId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Navigation( p => p.RoomTypes )
                .UsePropertyAccessMode( PropertyAccessMode.Field );

            builder.Metadata.FindNavigation( nameof( Property.RoomTypes ) )!
                .SetField( "_roomTypes" );
        }
    }
}