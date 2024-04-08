using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Infrastructure.Data.Repository;

internal sealed class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking> InsertBookingAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        _dbContext.Bookings.Add(booking);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return booking;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
