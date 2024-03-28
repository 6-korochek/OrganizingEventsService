using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class DeleteMemberEventHandler : IRequestHandler<DeleteMemberEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteMemberEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task Handle(DeleteMemberEventCommand request, CancellationToken cancellationToken)
    {
        var eventParticipantQuery = new EventParticipantQuery().WithEventId(request.EventId).WithAccountId(request.AccountId);
        var eventParticipantEntity = await _eventRepository.GetParticipantListByQuery(eventParticipantQuery).FirstAsync();
        await _eventRepository.DeleteParticipant(eventParticipantEntity);
    }
}