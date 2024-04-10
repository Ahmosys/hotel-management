namespace HotelManagement.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string from, string subject, string body);
}
