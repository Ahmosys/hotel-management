namespace HotelManagement.Application.Bookings.Commands.CheckInBooking;

public class CheckInBookingCommandValidator : AbstractValidator<CheckInBookingCommand>
{
    public CheckInBookingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
