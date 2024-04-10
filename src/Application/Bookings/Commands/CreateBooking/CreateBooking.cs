using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand : IRequest<int>
{
    public int RoomId { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public bool PayDirectly { get; init; }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;

    public CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository
    )
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);

        Guard.Against.NotFound(request.RoomId, room);

        var booking = Booking.Create(request.StartDate, request.EndDate, room);

        // TODO: If the booking is paid directly, process payment from payment gateway via Pay() method in Booking entity
        if (request.PayDirectly)
            // booking.Pay();

        await _bookingRepository.InsertBookingAsync(booking, cancellationToken);

        await _bookingRepository.SaveChangesAsync(cancellationToken);

        // Not sure if we should return the booking ID or the booking itself but
        // for now, we will return the booking ID bcz it's define like this in RFC
        // (https://www.rfc-editor.org/rfc/rfc7231#section-4.3.3)
        return booking.Id;
    }
}
