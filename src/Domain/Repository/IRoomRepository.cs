namespace HotelManagement.Domain.Repository;

public interface IRoomRepository
{
    Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default);

    Task<List<Room>> GetAvailableRoomsAsync(DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken = default);
  }
