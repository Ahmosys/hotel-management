using HotelManagement.Application.Bookings.Commands.CreateBooking;
using HotelManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Endpoints;

public class Bookings : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateBooking);
    }

    public async Task<Booking> CreateBooking(ISender sender, [FromBody] CreateBookingCommand command)
    {
        return await sender.Send(command);
    }
}
