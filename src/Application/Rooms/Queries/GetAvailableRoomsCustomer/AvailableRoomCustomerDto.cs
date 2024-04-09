using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;

namespace HotelManagement.Application.Rooms.Queries.GetAvailableRoomsCustomer;

public class AvailableRoomCustomerDto
{
    public int Id { get; init; }

    public int Capacity { get; init; }

    public decimal Rate { get; init; }

    public RoomType Type { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Room, AvailableRoomCustomerDto>();
        }
    }
}
