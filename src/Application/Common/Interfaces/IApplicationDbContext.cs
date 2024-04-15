using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Room> Rooms { get; }

    DbSet<Booking> Bookings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
