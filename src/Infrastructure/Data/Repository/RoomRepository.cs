using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
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
            .Where(r => !r.Bookings.Any(booking =>
                (startDate < booking.EndDate && endDate > booking.StartDate)))
            .ToListAsync(cancellationToken);

        return roomsAvailable;
    }

    public async Task<Room?> GetRoomByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var room = await _dbContext.Rooms
            .Include(r => r.Bookings)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        return room;
    }

    public async Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default)
    {
        var roomsWithBookings = await _dbContext.Rooms
            .Where(r => r.Bookings.Any())
            .Include(r => r.Bookings)
            .ToListAsync(cancellationToken);

        return roomsWithBookings;
    }

    public Task<List<Room>> GetRoomsToCleanAsync(CancellationToken cancellationToken = default)
    {
        var roomsToClean = _dbContext.Rooms
            .Where(r => r.IsClean == false)
            .ToListAsync(cancellationToken);

        return roomsToClean;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

