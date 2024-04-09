using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Rooms.Queries.GetAvailableRoomsCustomer;

[Authorize(Roles = Roles.Customer)]
public record GetAvailableRoomsCustomerQuery : IRequest<List<AvailableRoomCustomerDto>>
{
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
}

public class GetAvailableRoomsCustomerQueryHandler : IRequestHandler<GetAvailableRoomsCustomerQuery, List<AvailableRoomCustomerDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;


    public GetAvailableRoomsCustomerQueryHandler(
        IRoomRepository roomRepository,
        IMapper mapper
    )
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<List<AvailableRoomCustomerDto>> Handle(GetAvailableRoomsCustomerQuery request, CancellationToken cancellationToken)
    {
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(request.StartDate, request.EndDate, cancellationToken);
        return _mapper.Map<List<AvailableRoomCustomerDto>>(availableRooms);
    }
}
