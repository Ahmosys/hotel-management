using HotelManagement.Application.Rooms.Commands.MarkRoomAsClean;
using HotelManagement.Application.Rooms.Queries.GetAvailableRoomsCustomer;
using HotelManagement.Application.Rooms.Queries.GetAvailableRoomsReceptionist;
using HotelManagement.Application.Rooms.Queries.GetRoomsToClean;

namespace HotelManagement.Web.Endpoints;

public class Rooms : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAvailableRoomsCustomer, "available/customer/")
            .MapGet(GetAvailableRoomsReceptionist, "available/receptionist/")
            .MapGet(GetRoomsToClean, "to-clean/")
            .MapPut(MarkRoomAsClean, "{id}/clean");
    }

    public Task<List<AvailableRoomCustomerDto>> GetAvailableRoomsCustomer(ISender sender, [AsParameters] GetAvailableRoomsCustomerQuery query)
    {
        // We create two differents endpoint for get available rooms for customer and for receptionist
        // See: https://www.reddit.com/r/dotnet/comments/1620w9s/service_design_in_aspnet_core_using_different/
        return sender.Send(query);
    }

    public Task<List<AvailableRoomReceptionistDto>> GetAvailableRoomsReceptionist(ISender sender, [AsParameters] GetAvailableRoomsReceptionistQuery query)
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
