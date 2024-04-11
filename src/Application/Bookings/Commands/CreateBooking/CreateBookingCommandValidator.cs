namespace HotelManagement.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be greater than StartDate.");

        /* RuleFor(x => x.PayDirectly)
            .NotEmpty().WithMessage("PayDirectly is required."); */

        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("CardNumber is required if you pay directly.")
            .When(x => x.PayDirectly);

        RuleFor(x => x.ExpiryDate)
            .NotEmpty().WithMessage("ExpiryDate is required if you pay directly.")
            .When(x => x.PayDirectly);
    }
}
