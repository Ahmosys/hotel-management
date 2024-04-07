namespace HotelManagement.Domain.Entities;

public class Room : BaseAuditableEntity
{
    #region Properties
    private readonly List<Booking> _bookings = new();

    public int Capacity { get; private set; }

    public decimal Rate { get; private set; }

    public RoomStatus Status { get; private set; }
    
    public RoomType Type { get; private set; }

    public bool IsClean { get; private set; }

    public bool IsAvailable { get; private set; }

    public IReadOnlyCollection<Booking> Bookings => _bookings;

    #endregion

    #region Constructors

    private Room() { }

    // Use a static factory method to create a new room
    public static Room Create(int capacity, RoomStatus status, RoomType type, bool isClean, bool isAvailable)
    {
        // Check if the capacity is greater than zero
        if (capacity <= 0)
            throw new DomainException("Capacity must be greater than zero.");

        // Create a new room
        var room = new Room
        {
            Capacity = capacity,
            Status = status,
            Type = type,
            IsClean = isClean,
            IsAvailable = isAvailable
        };

        // Calculate the rate of the room based on the type
        room.Rate = room.CalculateRate();

        return room;
    }

    #endregion

    #region Methods

    private decimal CalculateRate()
    {
        // Here we use switch expression to define the rate of the room based on the type enum
        return Type switch
        {
            RoomType.Simple => 100,
            RoomType.Double => 200,
            RoomType.Suite => 300,
            _ => 0
        };
    }

    public Booking CreateBooking(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        // Check if the start date is before the end date
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date.", nameof(startDate));

        // Check if the room is available for the given dates
        if (!IsAvailableFor(startDate, endDate))
            throw new InvalidOperationException("Room is not available for the given dates.");

        // Create a new booking
        var booking = Booking.Create(startDate, endDate, this);

        // Add the booking to the room
        AddBooking(booking);

        return booking;
    }

    private bool IsAvailableFor(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        return !Bookings.Any(booking =>
                (startDate >= booking.StartDate && startDate <= booking.EndDate) ||
                (endDate >= booking.StartDate && endDate <= booking.EndDate));
    }

    public void MarkAsClean()
    {
        if (IsClean)
            throw new DomainException("Room is already clean.");
        IsClean = true;
    }

    public void MarkAsDirty()
    {
        if (!IsClean)
            throw new DomainException("Room is already dirty.");
        IsClean = false;
    }

    public void MarkAsAvailable()
    {
        if (IsAvailable)
            throw new DomainException("Room is already available.");
        IsAvailable = true;
    }

    public void MarkAsUnavailable()
    {
        if (!IsAvailable)
            throw new DomainException("Room is already unavailable.");
        IsAvailable = false;
    }

    // We don't need to have methods to delete a booking bcz we have already a property isCancelled in Booking entity
    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    #endregion
}
