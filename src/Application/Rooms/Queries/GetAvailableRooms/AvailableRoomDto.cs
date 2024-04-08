using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;

namespace HotelManagement.Application.Rooms.Queries.GetAvailableRooms;

public class AvailableRoomDto
{
    public int Id { get; init; }

    public int Capacity { get; init; }

    public decimal Rate { get; init; }

    public RoomType Type { get; init; }

    private class Mapping : Profile
    {
        // We don't want to expose the entire Room entity to the customer
        // We only want to expose the properties that are relevant to the customer
        public Mapping()
        {
            CreateMap<Room, AvailableRoomDto>();
        }
    }
}
