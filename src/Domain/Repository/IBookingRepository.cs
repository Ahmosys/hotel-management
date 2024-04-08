namespace HotelManagement.Domain.Repository;

public interface IBookingRepository
{
    Task<Booking> InsertBookingAsync(Booking booking, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
