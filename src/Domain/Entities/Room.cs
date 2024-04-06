namespace HotelManagement.Domain.Entities;

public class Room : BaseAuditableEntity
{

    public Room(int capacity, RoomStatus status, RoomType type, bool isClean, bool isAvailable)
        : base()
    {
        Capacity = capacity;
        Status = status;
        Type = type;
        IsClean = isClean;
        IsAvailable = isAvailable;
        // The rate of the room is based on the type of room
        // so we never want to set it directly but rather calculate it
        Rate = CalculateRate();
    }

    public int Capacity { get; set; }

    public decimal Rate { get; private set; }

    public RoomStatus Status { get; set; }
    
    public RoomType Type { get; set; }

    public bool IsClean { get; set; }

    public bool IsAvailable { get; set; }

    public IList<Booking> Bookings { get; private set; } = new List<Booking>();

    public decimal CalculateRate()
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
}
