using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Domain.Repository;

internal sealed class RoomRepository : IRoomRepository
{

    private readonly ApplicationDbContext _dbContext;

    public RoomRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken = default)
    {
        var roomsAvailable = await _dbContext.Rooms
            .Where(r => r.IsAvailable && !r.Bookings.Any(booking =>
                (startDate < booking.EndDate && endDate > booking.StartDate)))
            .ToListAsync(cancellationToken);

        return roomsAvailable;
    }


    public async Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default)
    {
        var roomsWithBookings = await _dbContext.Rooms
            .Where(r => r.Bookings.Any())
            .Include(r => r.Bookings)
            .ToListAsync(cancellationToken);

        return roomsWithBookings;
    }
}

