using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Application.TodoLists.Queries.GetTodos;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.RoomsApp.Queries.GetRooms;
public class RoomListDto
{
    public RoomListDto()
    {
        Items = Array.Empty<RoomItemDto>();
    }

    public int Capacity { get; set; }

    public decimal Rate { get; private set; }

    public IReadOnlyCollection<RoomItemDto> Items { get; init; }

    /*
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TodoList, TodoListDto>();
        }
    }*/
}
