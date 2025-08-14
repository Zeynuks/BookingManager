using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
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

            builder.HasIndex( p => p.Name )
                .IsUnique();

            builder.HasMany(a => a.RoomTypes)
                .WithMany(r => r.Amenities);
        }
    }
}