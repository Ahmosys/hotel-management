namespace HotelManagement.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(v => v.RoomId)
            .NotEmpty().WithMessage("RoomId is required.");

        RuleFor(v => v.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(v => v.EndDate)
            .NotEmpty().WithMessage("EndDate is required.");
    }
}
