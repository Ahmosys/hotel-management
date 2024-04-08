namespace HotelManagement.Domain.Entities;

public class Room : BaseAuditableEntity
{
    #region Properties

    public int Capacity { get; private set; }

    public decimal Rate { get; private set; }

    public RoomStatus Status { get; private set; }

    public RoomType Type { get; private set; }

    public bool IsClean { get; private set; }

    public bool IsAvailable { get; private set; }

    public virtual IList<Booking> Bookings { get; private set; } = new List<Booking>();

    #endregion

    #region Constructors

    private Room() { }

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

    /// <summary>
    /// Calculate the rate of the room based on the type.
    /// </summary>
    private decimal CalculateRate()
    {
        return Type switch
        {
            RoomType.Simple => 100,
            RoomType.Double => 200,
            RoomType.Suite => 300,
            _ => 0
        };
    }

    /// <summary>
    /// Check if the room is available for the given dates.
    /// </summary>
    public bool IsAvailableFor(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        // TODO: Need to activate eadger loading otherwise it will not load all the bookings
        return !Bookings.Any(booking =>
                (startDate >= booking.StartDate && startDate <= booking.EndDate) ||
                (endDate >= booking.StartDate && endDate <= booking.EndDate));
    }

    /// <summary>
    /// Mark the room as clean when the cleaner cleans the room.
    /// </summary>
    public void MarkAsClean()
    {
        if (IsClean)
            throw new DomainException("Room is already clean.");
        IsClean = true;
    }

    /// <summary>
    /// Mark the room as dirty when the customer checks out.
    /// </summary>
    /// <exception cref="DomainException"></exception>
    public void MarkAsDirty()
    {
        if (!IsClean)
            throw new DomainException("Room is already dirty.");
        IsClean = false;
    }

    /// <summary>
    /// Mark the room as available when the customer checks out.
    /// </summary>
    /// <exception cref="DomainException"></exception>
    public void MarkAsAvailable()
    {
        if (IsAvailable)
            throw new DomainException("Room is already available.");
        IsAvailable = true;
    }

    /// <summary>
    /// Mark the room as unavailable when the customer checks in.
    /// </summary>
    /// <exception cref="DomainException"></exception>
    public void MarkAsUnavailable()
    {
        if (!IsAvailable)
            throw new DomainException("Room is already unavailable.");
        IsAvailable = false;
    }
    #endregion
}
