using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.RoomsApp.Queries.GetRooms;
public class RoomItemDto
{
    public int Capacity { get; set; }

    public decimal Rate { get; private set; }
    
    /*
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Room, RoomItemDto>().ForMember(d => d.Priority,
                opt => opt.MapFrom(s => (int)s.Priority));
        }
    }*/
}
