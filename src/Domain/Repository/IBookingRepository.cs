namespace HotelManagement.Domain.Repository;

public interface IBookingRepository
{
    Task<Booking> InsertBookingAsync(Booking booking, CancellationToken cancellationToken = default);

    Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<Booking>?> GetBookingsStartingTommorowAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
