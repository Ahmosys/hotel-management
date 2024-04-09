using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;

namespace HotelManagement.Application.Rooms.Queries.GetAvailableRoomsReceptionist;

public class AvailableRoomReceptionistDto
{
    public int Id { get; init; }

    public int Capacity { get; init; }

    public decimal Rate { get; init; }

    public RoomType Type { get; init; }

    // The receptionist can see the status of the room
    public RoomStatus Status { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Room, AvailableRoomReceptionistDto>();
        }
    }
}
