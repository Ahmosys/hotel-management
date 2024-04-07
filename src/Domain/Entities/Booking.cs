namespace HotelManagement.Domain.Entities;

public class Booking : BaseAuditableEntity
{

    #region Properties

    // The start date and end date of the booking
    // can't be changed after the booking is created
    public DateTimeOffset StartDate { get; private set; }

    public DateTimeOffset EndDate { get; private set; }

    // By default a booking is not paid directly
    public bool IsPaid { get; private set; } = false;

    // By default a booking is not cancelled
    public bool IsCancelled { get; private set; } = false;

    public Room Room { get; private set; } = null!;

    #endregion

    #region Constructors

    private Booking() { }

    // Use a static factory method to create a new booking
    public static Booking Create(DateTimeOffset startDate, DateTimeOffset endDate, Room room)
    {
        // Create a new booking
        var booking = new Booking
        {
            StartDate = startDate,
            EndDate = endDate,
            Room = room
        };

        return booking;
    }

    #endregion

    #region Methods

    // Checks if a booking is refundable
    // If the booking is within 48 hours of the start date, it is not refundable
    public bool IsRefundable()
    {
        return StartDate.Subtract(DateTime.Now).TotalHours <= 48;
    }

    // Pay the booking
    public void MarkAsPay()
    {
        IsPaid = true;
    }

    // Cancel the booking
    public void MarkAsCancelled()
    {
        IsCancelled = true;
    }

    #endregion
}
