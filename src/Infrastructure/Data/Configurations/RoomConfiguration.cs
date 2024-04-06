using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Infrastructure.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(r => r.Rate)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(r => r.Type)
            .IsRequired();

        builder.Property(r => r.Status)
            .IsRequired();

        builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Room);
    }
}
