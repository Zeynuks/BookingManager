using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
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

            builder.HasIndex( p => p.Name )
                .IsUnique();

            builder.HasMany(s => s.RoomTypes)
                .WithMany(r => r.Services);
        }
    }
}