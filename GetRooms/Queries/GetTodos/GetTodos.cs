using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.GetRooms.Queries.GetTodos;

public record GetTodosQuery : IRequest<RoomsVm>
{
}

public class GetTodosQueryValidator : AbstractValidator<GetTodosQuery>
{
    public GetTodosQueryValidator()
    {
    }
}

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, RoomsVm>
{
    private readonly IApplicationDbContext _context;

    public GetTodosQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoomsVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
