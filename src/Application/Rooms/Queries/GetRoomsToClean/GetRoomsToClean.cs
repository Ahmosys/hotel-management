using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Rooms.Queries.GetRoomsToClean;

[Authorize(Roles = Roles.Cleaner)]
public record GetRoomsToCleanQuery : IRequest<List<RoomToCleanDto>>;

public class GetRoomsToCleanQueryHandler : IRequestHandler<GetRoomsToCleanQuery, List<RoomToCleanDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public GetRoomsToCleanQueryHandler(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<List<RoomToCleanDto>> Handle(GetRoomsToCleanQuery request, CancellationToken cancellationToken)
    {
        var roomsToClean = await _roomRepository.GetRoomsToCleanAsync();
        return _mapper.Map<List<RoomToCleanDto>>(roomsToClean);
    }
}
