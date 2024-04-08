namespace HotelManagement.Application.Rooms.Commands.MarkRoomAsClean;

public class MarkRoomAsCleanCommandValidator : AbstractValidator<MarkRoomAsCleanCommand>
{
    public MarkRoomAsCleanCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
