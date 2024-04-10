using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Bookings.EventHandlers;

public class BookingCheckedOutEventHandler : INotificationHandler<BookingCheckedOutEvent>
{
    private readonly ILogger<BookingCheckedOutEventHandler> _logger;
    private readonly IEmailService _emailService;
    private readonly IIdentityService _identityService;

    public BookingCheckedOutEventHandler(
        ILogger<BookingCheckedOutEventHandler> logger,
        IEmailService emailService,
        IIdentityService identityService
    )
    {
        _logger = logger;
        _emailService = emailService;
        _identityService = identityService;
    }

    public async Task Handle(BookingCheckedOutEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HotelManagement Domain Event: {DomainEvent}", notification.GetType().Name);

        // Retrive the user e-mail
        var userEmail = await _identityService.GetUserEmailAsync(notification.Booking.CreatedBy!);

        // Send an e-mail to the user
        await _emailService.SendEmailAsync(userEmail!, "admin@localhost", "Please rate your stay!", "Lorem ipsum elmet");
    }
}
