using HotelManagement.Application.Rooms.Queries.GetAvailableRooms;
using HotelManagement.Application.TodoLists.Queries.GetTodos;

namespace HotelManagement.Web.Endpoints;

public class Rooms : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAvailableRooms);
    }

    public Task<List<AvailableRoomDto>> GetAvailableRooms(ISender sender, [AsParameters] GetAvailableRoomsQuery query)
    {
        return sender.Send(query);
    }
}
