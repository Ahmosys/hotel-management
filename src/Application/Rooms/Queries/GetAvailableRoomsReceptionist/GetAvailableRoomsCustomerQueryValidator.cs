namespace HotelManagement.Application.Rooms.Queries.GetAvailableRoomsReceptionist;

public class GetAvailableRoomsReceptionistQueryValidator : AbstractValidator<GetAvailableRoomsReceptionistQuery>
{
    public GetAvailableRoomsReceptionistQueryValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be greater than StartDate.");
    }
}
