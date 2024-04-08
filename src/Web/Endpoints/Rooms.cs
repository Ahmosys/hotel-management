using HotelManagement.Application.Rooms.Commands.MarkRoomAsClean;
using HotelManagement.Application.Rooms.Queries.GetAvailableRooms;
using HotelManagement.Application.Rooms.Queries.GetRoomsToClean;

namespace HotelManagement.Web.Endpoints;

public class Rooms : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAvailableRooms, "available/")
            .MapGet(GetRoomsToClean, "to-clean/")
            .MapPut(MarkRoomAsClean, "{id}/clean");
    }

    public Task<List<AvailableRoomDto>> GetAvailableRooms(ISender sender, [AsParameters] GetAvailableRoomsQuery query)
    {
        return sender.Send(query);
    }

    public Task<List<RoomToCleanDto>> GetRoomsToClean(ISender sender)
    {
        return sender.Send(new GetRoomsToCleanQuery());
    }

    public async Task<IResult> MarkRoomAsClean(ISender sender, int id, MarkRoomAsCleanCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
