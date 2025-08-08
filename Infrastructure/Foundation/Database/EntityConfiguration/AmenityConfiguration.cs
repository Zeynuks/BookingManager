using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration
{
    internal class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure( EntityTypeBuilder<Amenity> builder )
        {
            builder.ToTable( nameof( Amenity ) )
                .HasKey( a => a.Id );

            builder.Property( a => a.Name )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.HasMany( a => a.RoomTypes )
                .WithMany( r => r.Amenities )
                .UsingEntity<Dictionary<string, object>>(
                    "AmenityToRoomType",
                    j => j.HasOne<RoomType>().WithMany().HasForeignKey( "RoomTypeId" ),
                    j => j.HasOne<Amenity>().WithMany().HasForeignKey( "AmenityId" )
                );
        }
    }
}