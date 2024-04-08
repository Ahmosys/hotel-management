using HotelManagement.Application.Rooms.Queries.GetAvailableRooms;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Endpoints;

public class Rooms : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAvailableRooms, "available/");
    }

    public Task<List<AvailableRoomDto>> GetAvailableRooms(ISender sender, [AsParameters] GetAvailableRoomsQuery query)
    {
        return sender.Send(query);
    }
}
