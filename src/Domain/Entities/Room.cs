namespace HotelManagement.Domain.Entities;

public class Room : BaseAuditableEntity
{
    #region Properties

    public int Capacity { get; set; }

    public decimal Rate { get; private set; }

    public RoomStatus Status { get; set; }
    
    public RoomType Type { get; set; }

    public bool IsClean { get; private set; }

    public bool IsAvailable { get; private set; }

    public IList<Booking> Bookings { get; private set; } = new List<Booking>();

    #endregion

    #region Constructors

    private Room() { }

    // Use a static factory method to create a new room
    public static Room Create(int capacity, RoomStatus status, RoomType type, bool isClean, bool isAvailable)
    {
        // Check if the capacity is greater than zero
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

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

    public void MarkAsClean()
    {
        IsClean = true;
    }

    public void MarkAsDirty()
    {
        IsClean = false;
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }

    public void MarkAsUnavailable()
    {
        IsAvailable = false;
    }

    public void AddBooking(Booking booking)
    {
        Bookings.Add(booking);
    }

    public void RemoveBooking(Booking booking)
    {
        Bookings.Remove(booking);
    }

    #endregion
}
