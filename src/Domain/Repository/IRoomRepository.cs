namespace HotelManagement.Domain.Repository;

public interface IRoomRepository
{
    Task<Room?> GetRoomByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default);

    Task<List<Room>> GetAvailableRoomsAsync(DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken = default);

    Task<List<Room>> GetRoomsToCleanAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
