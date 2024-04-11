using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Application.Common.Models;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Repository;

namespace HotelManagement.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand : IRequest<int>
{
    public int RoomId { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public bool PayDirectly { get; init; }
    public string? CardNumber { get; init; }
    public string? ExpiryDate { get; init; }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IPaymentGateway _paymentGateway;

    public CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IPaymentGateway paymentGateway
    )
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _paymentGateway = paymentGateway;
    }

    public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // Here I'll find a SHUUUU room for Billy, Billy needs to be able to do his business
        var room = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);

        // Damn, the room Billy wanted to book doesn't exist
        Guard.Against.NotFound(request.RoomId, room);

        // OK, this time the room he has chosen exists, we can go on and book it.
        var booking = Booking.Create(request.StartDate, request.EndDate, room);

        // Gosh, billy's got so much money he wants to pay directly in cash
        if (request.PayDirectly)
        {
            var paymentInfo = new PaymentInfo
            {
                CardNumber = request.CardNumber,
                ExpiryDate = request.ExpiryDate,
                Amount = booking.CalculateTotalAmount().ToString()
            };

            bool paymentSuccess = await _paymentGateway.ProcessPaymentAsync(paymentInfo);

            if (!paymentSuccess)
                throw new Exception("Payment failed.");

            booking.Pay();
        }

        await _bookingRepository.InsertBookingAsync(booking, cancellationToken);
        await _bookingRepository.SaveChangesAsync(cancellationToken);

        // Not sure if we should return the booking ID or the booking itself but
        // for now, we will return the booking ID bcz it's define like this in RFC
        // See: https://www.rfc-editor.org/rfc/rfc7231#section-4.3.3
        return booking.Id;
    }
}
