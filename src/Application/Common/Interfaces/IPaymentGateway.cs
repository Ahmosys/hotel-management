using HotelManagement.Application.Common.Models;

namespace HotelManagement.Application.Common.Interfaces;

public interface IPaymentGateway
{
    Task<bool> ProcessPaymentAsync(PaymentInfo paymentInfo);
}
