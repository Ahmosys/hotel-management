using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Rooms.Queries.GetAvailableRooms;

[Authorize(Roles = Roles.Customer)]
public record GetAvailableRoomsQuery : IRequest<List<AvailableRoomDto>>
{
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
}

public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, List<AvailableRoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public GetAvailableRoomsQueryHandler(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<List<AvailableRoomDto>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(request.StartDate, request.EndDate, cancellationToken);
        return _mapper.Map<List<AvailableRoomDto>>(availableRooms);
    }
}
