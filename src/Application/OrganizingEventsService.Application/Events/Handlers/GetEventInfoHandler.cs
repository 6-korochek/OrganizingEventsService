using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.ApplicationConstants;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Events.Handlers;

public class GetEventInfoHandler : IRequestHandler<GetEventInfoEventQuery, EventDto>
{
    private readonly IEventRepository _eventRepository;

    public GetEventInfoHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(GetEventInfoEventQuery request, CancellationToken cancellationToken)
    {
        Event eventEntity = null!;
        if (request.InviteCode != null)
        {
            eventEntity = await _eventRepository.GetEventByInviteCode(request.InviteCode);
        } else if (request.EventId != null)
        {
            eventEntity = await _eventRepository.GetById((Guid)request.EventId);
        }
        
        var query = new EventParticipantQuery()
            .WithEventId(eventEntity.Id)
            .WithRoleId(Roles.ORGANIZER)
            .WithAccount()
            .WithLimit(1);

        var organizer = await _eventRepository.GetParticipantListByQuery(query).FirstAsync(cancellationToken: cancellationToken);
        return new EventDto // Это п*зд*ц заменим на AutoMapper
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            MeetingLink = eventEntity.MeetingLink,
            InviteCode = eventEntity.InviteCode,
            Status = eventEntity.Status,
            MaxParticipant = eventEntity.MaxParticipant,
            StartDatetime = eventEntity.StartDatetime,
            EndDatetime = eventEntity.EndDatetime,
            Organizer = new OrganizerDto
            {
                AccountId = organizer!.AccountIdNavigation!.Id,
                Name = organizer.AccountIdNavigation.Name,
                Surname = organizer.AccountIdNavigation.Surname
            }
        };
    }
}
