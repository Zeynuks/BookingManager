using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration
{
    internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure( EntityTypeBuilder<RoomType> builder )
        {
            builder.ToTable( nameof( RoomType ) )
                .HasKey( r => r.Id );

            builder.Property( r => r.Name )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( r => r.DailyPrice )
                .HasPrecision( 18, 2 )
                .IsRequired();

            builder.Property( r => r.MinPersonCount )
                .IsRequired();

            builder.Property( r => r.MaxPersonCount )
                .IsRequired();

            builder.Property( r => r.Currency )
                .HasMaxLength( 10 )
                .IsRequired();

            builder.HasIndex( r => r.PropertyId );

            builder.HasOne( r => r.Property )
                .WithMany()
                .HasForeignKey( r => r.PropertyId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasMany( r => r.Amenities )
                .WithMany( a => a.RoomTypes )
                .UsingEntity<Dictionary<string, object>>(
                    "RoomTypeToAmenity",
                    j => j.HasOne<Amenity>().WithMany().HasForeignKey( "AmenityId" ),
                    j => j.HasOne<RoomType>().WithMany().HasForeignKey( "RoomTypeId" )
                );
        }
    }
}