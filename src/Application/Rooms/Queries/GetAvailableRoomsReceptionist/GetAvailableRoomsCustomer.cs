using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Rooms.Queries.GetAvailableRoomsReceptionist;

[Authorize(Roles = Roles.Receptionist)]
public record GetAvailableRoomsReceptionistQuery : IRequest<List<AvailableRoomReceptionistDto>>
{
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
}

public class GetAvailableRoomsReceptionistQueryHandler : IRequestHandler<GetAvailableRoomsReceptionistQuery, List<AvailableRoomReceptionistDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;


    public GetAvailableRoomsReceptionistQueryHandler(
        IRoomRepository roomRepository,
        IMapper mapper
    )
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<List<AvailableRoomReceptionistDto>> Handle(GetAvailableRoomsReceptionistQuery request, CancellationToken cancellationToken)
    {
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(request.StartDate, request.EndDate, cancellationToken);
        return _mapper.Map<List<AvailableRoomReceptionistDto>>(availableRooms);
    }
}
