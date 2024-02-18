using OrganizingEventsService.Application.Contracts.Common;
using OrganizingEventsService.Application.Contracts.Event;
using OrganizingEventsService.Application.Contracts.Feedback;
using OrganizingEventsService.Application.Contracts.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Contracts.Services;

public abstract class EventService
{
    protected IAccountService AccountService { get; }
    
    protected EventService(IAccountService accountService)
    {
        AccountService = accountService;
    }

    public abstract EventDto GetEventInfo(Guid? eventId, string? inviteCode);
    
    public abstract IEnumerable<EventDto> GetEventsWhereAccountIsParticipant(
        Guid accountId,
        EventParticipantInviteStatus eventParticipant,
        PaginationDto paginationDto);
    
    public abstract NewEventDto CreateEvent(Guid organizerId, CreateEventDto createEventDto);

    public abstract Guid PartiallyUpdateEvent(Guid eventId, UpdateEventDto updateEventDto);

    public abstract void DeleteEventById(Guid eventId);

    public abstract void UpdateParticipantStatus(
        Guid currentAccountId,
        UpdateParticipantStatusDto updateInvitationStatusDto);

    public abstract IEnumerable<ParticipantDto> GetParticipants(Guid eventId);

    public abstract ParticipantDto GetParticipantById(Guid participantId);

    public abstract void CreateParticipants(IEnumerable<CreateParticipantDto> createParticipantDtos);

    public abstract ParticipantDto PartiallyUpdateParticipant(
        Guid participantId,
        UpdateParticipantDto updateParticipantDto);

    public abstract void DeleteParticipantsByEmails(IEnumerable<string> accountEmails);
    
    public abstract void DeleteParticipantByAccountId(Guid currentAccountId);
    
    public abstract IEnumerable<FeedbackDto> GetFeedbacksByEventId(Guid eventId, Guid currentAccountId);
    
    public abstract Guid CreateFeedback(Guid eventId, Guid currentAccountId, CreateFeedbackDto createFeedbackDto);

    public abstract FeedbackDto PartiallyUpdateFeedback(
        Guid feedbackId,
        Guid currentAccountId,
        UpdateFeedbackDto updateFeedbackDto);
    
    public abstract void DeleteFeedbackById(Guid feedbackId, Guid currentAccountId);
}