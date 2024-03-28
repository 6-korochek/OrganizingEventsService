using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Dto.Role;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class GetMembersEventHandler : IRequestHandler<GetMembersEventQuery, IAsyncEnumerable<ParticipantDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetMembersEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<IAsyncEnumerable<ParticipantDto>> Handle(GetMembersEventQuery request, CancellationToken cancellationToken)
    {
        var eventEntity =  await _eventRepository.GetById(request.EventId);
        var query = new EventParticipantQuery().WithEventId(eventEntity.Id).WithAccount().WithRole();
        var eventParticipantEntities = _eventRepository.GetParticipantListByQuery(query);
        var participants = from e in eventParticipantEntities
            select new ParticipantDto()
            {
                AccountId = e.AccountId,
                AccountName = e!.AccountIdNavigation!.Name,
                AccountSurName = e!.AccountIdNavigation!.Surname,
                Role = new RoleDto()
                {
                    Id = e.RoleId,
                    Name = e!.RoleIdNavigation!.Name
                },
                Status = e.InviteStatus
            };

        return participants;
    }
}