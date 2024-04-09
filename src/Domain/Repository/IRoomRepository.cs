namespace HotelManagement.Domain.Repository;

public interface IRoomRepository
{
    Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default);

    Task<List<Room>> GetRoomsToCleanAsync(CancellationToken cancellationToken = default);
 }
