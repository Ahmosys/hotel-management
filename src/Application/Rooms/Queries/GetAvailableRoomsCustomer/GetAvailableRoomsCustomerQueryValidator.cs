namespace HotelManagement.Application.Rooms.Queries.GetAvailableRoomsCustomer;

public class GetAvailableRoomsCustomerQueryValidator : AbstractValidator<GetAvailableRoomsCustomerQuery>
{
    public GetAvailableRoomsCustomerQueryValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be greater than StartDate.");
    }
}
