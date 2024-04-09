using HotelManagement.Application.Bookings.Commands.CancelBooking;
using HotelManagement.Application.Bookings.Commands.CheckInBooking;
using HotelManagement.Application.Bookings.Commands.CheckOutBooking;
using HotelManagement.Application.Bookings.Commands.CreateBooking;
using HotelManagement.Application.Rooms.Commands.MarkRoomAsClean;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Endpoints;

public class Bookings : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateBooking)
            .MapPut(CancelBooking, "{id}/cancel")
            .MapPut(CheckInBooking, "{id}/check-in")
            .MapPut(CheckOutBooking, "{id}/check-out");
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

    public async Task<IResult> CheckInBooking(ISender sender, int id, CheckInBookingCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> CheckOutBooking(ISender sender, int id, CheckOutBookingCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
