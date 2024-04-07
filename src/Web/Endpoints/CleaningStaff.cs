using HotelManagement.Application.TodoLists.Queries.GetTodos;
using HotelManagement.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace HotelManagement.Web.Endpoints;

public class CleaningStaff
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRoomsToClean);
    }

    public Task<TodosVm> GetRoomsToClean(ISender sender)
    {
        return sender.Send(new GetTodosQuery());
    }
}
