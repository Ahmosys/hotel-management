namespace HotelManagement.Application.Rooms.Queries.GetAvailableRooms;

public class GetAvailableRoomsQueryValidator : AbstractValidator<GetAvailableRoomsQuery>
{
    public GetAvailableRoomsQueryValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be greater than StartDate.");
    }
}
