using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class GetMyEventHandler : IRequestHandler<GetMyEventQuery, IAsyncEnumerable<EventDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetMyEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<IAsyncEnumerable<EventDto>> Handle(GetMyEventQuery request, CancellationToken cancellationToken)
    {
        var query = new EventQuery()
            .WithStatus(request.Status)
            .WithAccountParticipantId(request.AccountId);

        if (request.EventRequest != null)
        {
            query
                .WithLimit(request.EventRequest.Limit)
                .WithOffset(request.EventRequest.Offset);
        }
        var events = await _eventRepository.GetListByQuery(query).ToListAsync();
        var eventsDtos= events
            .Select(eventEntity => new EventDto // Это п*зд*ц заменим на AutoMapper
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                MeetingLink = eventEntity.MeetingLink,
                InviteCode = eventEntity.InviteCode,
                Status = eventEntity.Status,
                MaxParticipant = eventEntity.MaxParticipant,
                StartDatetime = eventEntity.StartDatetime,
                EndDatetime = eventEntity.EndDatetime
            });

        return (IAsyncEnumerable<EventDto>)eventsDtos;
    }
}