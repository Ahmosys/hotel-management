using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Application.Common.Security;
using HotelManagement.Domain.Constants;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Rooms.Commands.MarkRoomAsClean;

[Authorize(Roles = Roles.Cleaner)]
public record MarkRoomAsCleanCommand : IRequest
{
    public int Id { get; init; }
}

public class MarkRoomAsCleanCommandHandler : IRequestHandler<MarkRoomAsCleanCommand>
{
    private readonly IRoomRepository _roomRepository;

    public MarkRoomAsCleanCommandHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task Handle(MarkRoomAsCleanCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roomRepository.GetRoomByIdAsync(request.Id);

        Guard.Against.NotFound(request.Id, entity);

        entity.MarkAsClean();

        await _roomRepository.SaveChangesAsync(cancellationToken);
    }
}
