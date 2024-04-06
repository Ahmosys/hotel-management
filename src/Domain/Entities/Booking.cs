namespace HotelManagement.Domain.Entities;

public class Booking : BaseAuditableEntity
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPaid { get; set; }

    public bool IsCancelled { get; set; }

    public bool IsRefundable { get; set; }

    public Room Room { get; set; } = null!;
}
