using HotelManagement.Application.Common.Exceptions;
using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Bookings.Commands.CancelBooking;

[Authorize(Roles = Roles.Customer)]
public record CancelBookingCommand : IRequest
{
    public int Id { get; init; }
}

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUser _user;

    public CancelBookingCommandHandler(
        IBookingRepository bookingRepository,
        IUser user
    )
    {
        _bookingRepository = bookingRepository;
        _user = user;
    }

    public async Task Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetBookingByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, booking);

        // Only the user who created the booking can only cancel it
        if (booking.CreatedBy != _user.Id)
            throw new ForbiddenAccessException();

        booking.Cancel();

        await _bookingRepository.SaveChangesAsync(cancellationToken);
    }
}

