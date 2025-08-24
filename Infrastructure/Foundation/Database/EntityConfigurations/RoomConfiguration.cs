using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure( EntityTypeBuilder<Room> builder )
        {
            builder.ToTable( nameof( Room ) )
                .HasKey( r => r.Id );

            builder.Property( r => r.Number )
                .HasMaxLength( 50 )
                .IsRequired();

            builder.HasIndex( r => r.RoomTypeId );

            builder.HasIndex( r => new
            {
                r.RoomTypeId,
                r.Number
            } ).IsUnique();

            builder.HasMany( r => r.Reservations )
                .WithOne( x => x.Room )
                .HasForeignKey( x => x.RoomId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Navigation( r => r.Reservations )
                .UsePropertyAccessMode( PropertyAccessMode.Field );
            builder.Metadata.FindNavigation( nameof( Room.Reservations ) )!
                .SetField( "_reservations" );
        }
    }
}