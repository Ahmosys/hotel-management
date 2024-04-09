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

    public async Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Rooms.ToListAsync();
    }

    public async Task<List<Room>> GetRoomsToCleanAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Rooms
         .Where(room => !room.IsClean)
         .ToListAsync(cancellationToken);
    }
}

