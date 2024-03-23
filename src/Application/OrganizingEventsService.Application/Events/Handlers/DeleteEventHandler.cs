using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class DeleteEventHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        await _eventRepository.DeleteById(request.EventId);
    }
}