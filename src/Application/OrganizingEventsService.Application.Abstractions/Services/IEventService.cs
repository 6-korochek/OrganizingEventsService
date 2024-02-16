using OrganizingEventsService.Application.Contracts.Event;
using OrganizingEventsService.Application.Contracts.Feedback;
using OrganizingEventsService.Application.Contracts.Participant;

namespace OrganizingEventsService.Application.Abstractions.Services;

public interface IEventService
{
    EventDto GetEventByPk(Guid eventPk);

    EventDto GetEventByInviteCode(string inviteCode);
    
    NewEventDto CreateEvent(Guid organizerPk, CreateEventDto createEventDto);

    Guid PartiallyUpdateEvent(Guid eventPk, UpdateEventDto updateEventDto);

    void DeleteEventByPk(Guid eventPk);
}