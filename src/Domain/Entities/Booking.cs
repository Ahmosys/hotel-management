namespace HotelManagement.Domain.Entities;

public class Booking : BaseAuditableEntity
{

    #region Properties

    public DateTimeOffset StartDate { get; private set; }

    public DateTimeOffset EndDate { get; private set; }

    // By default a booking is not paid directly
    public bool IsPaid { get; private set; } = false;

    // By default a booking is not cancelled
    public bool IsCancelled { get; private set; } = false;

    // By default a booking is not refunded because it is not cancelled
    public bool IsRefunded { get; private set; } = false;

    public Room Room { get; private set; } = null!;

    #endregion

    #region Constructors

    private Booking() { }

    /// <summary>
    /// Create a new Booking with use of static factory method to ensure integrity and consistency.
    /// </summary>
    public static Booking Create(DateTimeOffset startDate, DateTimeOffset endDate, Room room)
    {
        // Check if the start date is before the end date
        if (startDate >= endDate)
            throw new DomainException("Start date must be before end date.");

        // Check if the room is available for the given dates
        if (!room.IsAvailableFor(startDate, endDate))
            throw new DomainException("Room is not available for the given dates.");

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

    /// <summary>
    /// Check if a booking is refundable by verify if the cancel date
    /// is less than 48 hours from the start date.
    /// </summary>
    public bool IsRefundable()
    {
        return StartDate.Subtract(DateTime.Now).TotalHours <= 48;
    }

    /// <summary>
    /// Pay for a booking by calling the payment gateway service.
    /// </summary>
    public void Pay()
    {
        if (IsPaid)
            throw new DomainException("Booking is already paid.");

        // TODO: Call here the fake payment gateway OR deplace the logic in specific domain service

        // Mark the booking as paid
        IsPaid = true;
    }

    /// <summary>
    /// Cancel a booking and refund the payment if it is refundable.
    /// </summary>
    public void Cancel()
    {
        if (IsCancelled)
            throw new DomainException("Booking is already cancelled.");

        if (!IsPaid)
            throw new DomainException("Booking must be paid to be cancelled.");

        if (IsRefundable())
            IsRefunded = true;

        IsCancelled = true;
    }

    /// <summary>
    /// Instantly cancel a booking without checking if it is refundable
    /// and refund the payment if required.
    /// </summary>
    public void InstantCancel(bool withRefund)
    {
        if (IsCancelled)
            throw new DomainException("Booking is already cancelled.");

        if (!IsPaid)
            throw new DomainException("Booking must be paid to be cancelled.");

        if (withRefund)
            IsRefunded = true;

        IsCancelled = true;
    }

    #endregion
}
