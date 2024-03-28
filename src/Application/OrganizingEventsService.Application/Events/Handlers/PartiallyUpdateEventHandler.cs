using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class PartiallyUpdateEventHandler : IRequestHandler<PartiallyUpdateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;

    public PartiallyUpdateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Guid> Handle(PartiallyUpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetById(request.EventId);
        if (eventEntity.Status is EventStatus.IsOver)
        {
            throw new Exception("Event is over!"); // Потом кастомные добавим
        }

        eventEntity.MeetingLink = request.EventRequest.MeetingLink ?? eventEntity.MeetingLink;
        if (eventEntity.Status is not EventStatus.InGoing)
        {
            eventEntity.Name = request.EventRequest.Name ?? eventEntity.Name;
            eventEntity.Description = request.EventRequest.Description ?? eventEntity.Description;
            eventEntity.MaxParticipant = request.EventRequest.MaxParticipant ?? eventEntity.MaxParticipant;
            eventEntity.StartDatetime = request.EventRequest.StartDate ?? eventEntity.StartDatetime;
            eventEntity.EndDatetime = request.EventRequest.EndDate ?? eventEntity.EndDatetime;
            eventEntity.Status = request.EventRequest.EventStatus ?? eventEntity.Status;

            await _eventRepository.Update(eventEntity);
            return eventEntity.Id;
        }

        if (request.EventRequest.EventStatus is EventStatus.IsOver)
        {
            eventEntity.Status = EventStatus.IsOver;
        }

        await _eventRepository.Update(eventEntity);
        return eventEntity.Id;
    }
}