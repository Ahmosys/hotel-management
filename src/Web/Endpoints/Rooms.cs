﻿using HotelManagement.Application.RoomsApp.Queries.GetRooms;
using HotelManagement.Application.TodoLists.Queries.GetTodos;
using HotelManagement.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace HotelManagement.Web.Endpoints;

public class Rooms : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRoomsList);
    }

    public Task<RoomsVm> GetRoomsList(ISender sender)
    {
        return sender.Send(new GetRoomsQuery());
    }
}
