using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Domain.Repository;
using Microsoft.Extensions.Logging;
using Quartz;

namespace HotelManagement.Infrastructure.Jobs;

public class PreStayNotificationJob : IJob
{
    private readonly ILogger<PreStayNotificationJob> _logger;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmailService _emailService;
    private readonly IIdentityService _identityService;

    public PreStayNotificationJob(
        ILogger<PreStayNotificationJob> logger,
        IBookingRepository bookingRepository,
        IEmailService emailService,
        IIdentityService identityService
    )
    {
        _logger = logger;
        _bookingRepository = bookingRepository;
        _emailService = emailService;
        _identityService = identityService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("HotelManagement Jobs: {JobName} is running", context.JobDetail.Key.Name);

        var bookingsToNotify = await _bookingRepository.GetBookingsStartingTommorowAsync();

        // If there are no bookings to notify
        if (bookingsToNotify!.Count == 0)
            return;

        // Send an e-mail to the users to notify them about their booking
        foreach (var booking in bookingsToNotify)
        {
            var userEmail = await _identityService.GetUserEmailAsync(booking.CreatedBy!);
            if (userEmail != null)
                await _emailService.SendEmailAsync(userEmail, "admin@localhost", "Your stay is tomorrow!", "Lorem ipsum elmet");
        }
    }
}
