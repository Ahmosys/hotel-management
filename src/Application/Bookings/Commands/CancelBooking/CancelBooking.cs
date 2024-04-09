using HotelManagement.Application.Common.Exceptions;
using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Bookings.Commands.CancelBooking;

[Authorize(Roles = Roles.Customer + ", " + Roles.Receptionist)]
public record CancelBookingCommand : IRequest
{
    public int Id { get; init; }
    public bool? WithRefund { get; init; }
}

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IIdentityService _identityService;
    private readonly IUser _user;

    public CancelBookingCommandHandler(
        IBookingRepository bookingRepository,
        IIdentityService identityService,
        IUser user
    )
    {
        _bookingRepository = bookingRepository;
        _identityService = identityService;
        _user = user;
    }

    public async Task Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetBookingByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, booking);

        // If the user is a customer, they can only cancel their own bookings and cannot refund them after 48 hours
        if (await _identityService.IsInRoleAsync(_user.Id!, Roles.Customer))
        {
            if (booking.CreatedBy != _user.Id)
                throw new ForbiddenAccessException();

            booking.Cancel();
        }
        // If the user is a receptionist, they can cancel any booking and refund them at any time
        else
        {
            booking.InstantCancel(request.WithRefund ?? false);
        }

        await _bookingRepository.SaveChangesAsync(cancellationToken);
    }
}

