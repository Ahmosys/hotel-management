using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Application.Common.Interfaces;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Events;

namespace HotelManagement.Application.Rooms.Commands.AddRoomToClean;

public record AddRoomToCleanCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class AddRoomToCleanCommandHandler : IRequestHandler<AddRoomToCleanCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddRoomToCleanCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddRoomToCleanCommand request, CancellationToken cancellationToken)
    {

        //Get the room entity

        var entity = new TodoItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
