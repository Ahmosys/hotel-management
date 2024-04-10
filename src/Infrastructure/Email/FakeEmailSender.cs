using HotelManagement.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Infrastructure.Services;
public class FakeEmailSender : IEmailService
{
    private readonly ILogger<FakeEmailSender> _logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string to, string from, string subject, string body)
    {
        _logger.LogInformation($"HotelManagement FakeEmailSender Service: Sending email to {to} from {from} with subject {subject} and body {body}");
        return Task.CompletedTask;
    }
}
