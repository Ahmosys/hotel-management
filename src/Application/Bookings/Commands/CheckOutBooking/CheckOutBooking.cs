﻿using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Bookings.Commands.CheckOutBooking;

[Authorize(Roles = Roles.Receptionist)]
public record CheckOutBookingCommand : IRequest
{
    public int Id { get; init; }
}

public class CheckOutCommandHandler : IRequestHandler<CheckOutBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;

    public CheckOutCommandHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task Handle(CheckOutBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetBookingByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, booking);

        booking.CheckOut();

        await _bookingRepository.SaveChangesAsync(cancellationToken);
    }
}
