using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration
{
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure( EntityTypeBuilder<Service> builder )
        {
            builder.ToTable( nameof( Service ) )
                .HasKey( s => s.Id );

            builder.Property( s => s.Name )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.HasMany( s => s.RoomTypes )
                .WithMany( r => r.Services )
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceToRoomType",
                    j => j.HasOne<RoomType>().WithMany().HasForeignKey( "RoomTypeId" ),
                    j => j.HasOne<Service>().WithMany().HasForeignKey( "ServiceId" )
                );
        }
    }
}