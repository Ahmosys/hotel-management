namespace HotelManagement.Domain.Events;

public class BookingCheckedOutEvent : BaseEvent
{
    // This event is raised when a booking is checked out
    public BookingCheckedOutEvent(Booking booking)
    {
        Booking = booking;
    }

    public Booking Booking { get; }
}
