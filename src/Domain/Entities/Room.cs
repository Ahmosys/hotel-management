namespace HotelManagement.Domain.Entities;

public class Room : BaseAuditableEntity
{
    public int Capacity { get; set; }

    // The rate depend on the room type
    public decimal Rate
    {
        get
        {
            return Type switch
            {
                RoomType.Simple => 100,
                RoomType.Double => 200,
                RoomType.Suite => 300,
                _ => 0
            };
        }
    }

    public RoomStatus Status { get; set; }

    public RoomType Type { get; set; }

    public bool IsOccupied { get; set; }

    public bool IsClean { get; set; }

    public bool IsAvailable { get; set; }

    public IList<Booking> Bookings { get; private set; } = new List<Booking>(); 
}
