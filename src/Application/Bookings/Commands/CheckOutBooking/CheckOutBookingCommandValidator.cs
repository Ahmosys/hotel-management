namespace HotelManagement.Application.Bookings.Commands.CheckOutBooking;

public class CheckOutBookingCommandValidator : AbstractValidator<CheckOutBookingCommand>
{
    public CheckOutBookingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
