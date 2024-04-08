using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;

namespace HotelManagement.Application.Rooms.Queries.GetRoomsToClean;

public class RoomToCleanDto
{
    public int Id { get; init; }

    public int Capacity { get; init; }

    public decimal Rate { get; init; }

    public RoomType Type { get; init; }

    public RoomStatus Status { get; init; }

    public bool IsClean { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Room, RoomToCleanDto>();
        }
    }
}
