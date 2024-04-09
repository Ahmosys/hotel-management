using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Bookings.Commands.CheckInBooking;

[Authorize(Roles = Roles.Receptionist)]
public record CheckInBookingCommand : IRequest
{
    public int Id { get; init; }
}

public class CheckInCommandHandler : IRequestHandler<CheckInBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;

    public CheckInCommandHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task Handle(CheckInBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetBookingByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, booking);

        booking.CheckIn();

        await _bookingRepository.SaveChangesAsync(cancellationToken);
    }
}
