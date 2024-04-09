using HotelManagement.Application.Common.Models;
using HotelManagement.Application.RoomsApp.Queries.GetRooms;

namespace HotelManagement.Application.RoomsApp.Queries.GetRooms;
public class RoomsToCleanVm
{
    public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<RoomListDto> Lists { get; init; } = Array.Empty<RoomListDto>();
}
