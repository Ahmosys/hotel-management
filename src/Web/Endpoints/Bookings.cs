using HotelManagement.Application.Bookings.Commands.CancelBooking;
using HotelManagement.Application.Bookings.Commands.CreateBooking;
using HotelManagement.Application.Rooms.Commands.MarkRoomAsClean;
using HotelManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Endpoints;

public class Bookings : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateBooking)
            .MapPut(CancelBooking, "{id}/cancel");
    }

    public async Task<IResult> CreateBooking(ISender sender, [FromBody] CreateBookingCommand command)
    {
        int bookingId = await sender.Send(command);
        return Results.Created($"/bookings/{bookingId}", bookingId);
    }

    public async Task<IResult> CancelBooking(ISender sender, int id, CancelBookingCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
