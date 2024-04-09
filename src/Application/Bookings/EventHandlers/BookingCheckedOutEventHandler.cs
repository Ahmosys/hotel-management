using HotelManagement.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Bookings.EventHandlers;

public class BookingCheckedOutEventHandler : INotificationHandler<BookingCheckedOutEvent>
{
    private readonly ILogger<BookingCheckedOutEventHandler> _logger;

    public BookingCheckedOutEventHandler(ILogger<BookingCheckedOutEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BookingCheckedOutEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HotelManagement Domain Event: {DomainEvent}", notification.GetType().Name);

        // TODO: Send an email to the customer to retrive her feedback (call the EmailService)

        return Task.CompletedTask;
    }
}
