using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
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

            builder.Property( r => r.IsSharedOccupancy )
                .IsRequired();

            builder.Property( r => r.MaxPlaces )
                .IsRequired();

            builder.HasIndex( r => r.PropertyId );

            builder.HasIndex( r => new
                {
                    r.PropertyId,
                    r.Name
                } )
                .IsUnique();

            builder.HasOne( r => r.Property )
                .WithMany( p => p.RoomTypes )
                .HasForeignKey( r => r.PropertyId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasMany( r => r.Rooms )
                .WithOne( x => x.RoomType )
                .HasForeignKey( x => x.RoomTypeId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Navigation( r => r.Rooms )
                .UsePropertyAccessMode( PropertyAccessMode.Field );
            builder.Metadata.FindNavigation( nameof( RoomType.Rooms ) )!
                .SetField( "_rooms" );

            builder.HasMany( r => r.Amenities )
                .WithMany( a => a.RoomTypes )
                .UsingEntity<Dictionary<string, object>>(
                    "RoomTypeToAmenity",
                    j => j.HasOne<Amenity>()
                        .WithMany()
                        .HasForeignKey( "AmenityId" )
                        .OnDelete( DeleteBehavior.Cascade ),
                    j => j.HasOne<RoomType>()
                        .WithMany()
                        .HasForeignKey( "RoomTypeId" )
                        .OnDelete( DeleteBehavior.Cascade ),
                    j =>
                    {
                        j.ToTable( "RoomTypeToAmenity" );
                        j.HasKey( "RoomTypeId", "AmenityId" );
                        j.HasIndex( "AmenityId" );
                    } );

            builder.HasMany( r => r.Services )
                .WithMany( s => s.RoomTypes )
                .UsingEntity<Dictionary<string, object>>(
                    "RoomTypeToService",
                    j => j.HasOne<Service>()
                        .WithMany()
                        .HasForeignKey( "ServiceId" )
                        .OnDelete( DeleteBehavior.Cascade ),
                    j => j.HasOne<RoomType>()
                        .WithMany()
                        .HasForeignKey( "RoomTypeId" )
                        .OnDelete( DeleteBehavior.Cascade ),
                    j =>
                    {
                        j.ToTable( "RoomTypeToService" );
                        j.HasKey( "RoomTypeId", "ServiceId" );
                        j.HasIndex( "ServiceId" );
                    } );
        }
    }
}