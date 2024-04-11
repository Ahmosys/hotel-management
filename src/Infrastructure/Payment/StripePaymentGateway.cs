using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Infrastructure.Payment;

public class StripePaymentGateway : IPaymentGateway
{
    private readonly ILogger<StripePaymentGateway> _logger;

    public StripePaymentGateway(ILogger<StripePaymentGateway> logger)
    {
        _logger = logger;
    }

    public Task<bool> ProcessPaymentAsync(PaymentInfo paymentInfo)
    {
        // Process the payment with Stripe
        _logger.LogInformation("HoteManagement Service: Processing payment with Stripe for {Amount}", paymentInfo.Amount);
        return Task.FromResult(true);
    }
}
