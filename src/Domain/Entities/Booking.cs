using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities;

public class Booking : BaseAuditableEntity
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    // By default a booking is not paid directly
    public bool IsPaid { get; set; } = false;

    // By default a booking is not cancelled
    public bool IsCancelled { get; set; } = false;

    public Room Room { get; set; } = null!;

    // Checks if a booking is refundable
    // If the booking is within 48 hours of the start date, it is not refundable
    public bool IsRefundable()
    {
        return StartDate.Subtract(DateTime.Now).TotalHours <= 48;
    }
}
