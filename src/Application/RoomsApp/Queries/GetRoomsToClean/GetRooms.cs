using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Application.Common.Models;
using HotelManagement.Application.Common.Security;
using HotelManagement.Application.TodoLists.Queries.GetTodos;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.RoomsApp.Queries.GetRooms;
[Authorize]
public record GetRoomsQuery : IRequest<RoomsToCleanVm>;

public class GetRoomsToCleanQueryHandler : IRequestHandler<GetRoomsQuery, RoomsToCleanVm>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomsToCleanQueryHandler(
        IRoomRepository roomRepository
    )
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomsToCleanVm> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {

        var rooms = await _roomRepository.GetRoomsToCleanAsync().Where(room => !room.IsClean)
        .ToListAsync(cancellationToken);


        // Mapper les entités Room vers les DTO RoomListDto
        var roomDtos = rooms.Select(RoomMapper.MapToRoomListDto).ToList();


        return new RoomsVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
               .Cast<PriorityLevel>()
               .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
            .ToList(),

            Lists = roomDtos
        };
    }
}
