using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class DeleteMembersEventHandler : IRequestHandler<DeleteMembersEventCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEventRepository _eventRepository;

    public DeleteMembersEventHandler(IAccountRepository accountRepository, IEventRepository eventRepository)
    {
        _accountRepository = accountRepository;
        _eventRepository = eventRepository;
    }


    public async Task Handle(DeleteMembersEventCommand request, CancellationToken cancellationToken)
    {
        var accountQuery = new AccountQuery().WithEmail(request.AccountEmails);
        var accountEntityList = await _accountRepository.GetListByQuery(accountQuery).ToListAsync();
        var eventParticipantQuery = new EventParticipantQuery().WithEventId(request.EventId);
        foreach (var accountEntity in accountEntityList)
        {
            eventParticipantQuery.WithAccountId(accountEntity.Id);
        }

        var eventParticipants = await _eventRepository.GetParticipantListByQuery(eventParticipantQuery).ToListAsync();
        await _eventRepository.DeleteParticipant(eventParticipants);
    }
}