using OrganizingEventsService.Application.Contracts.Common;
using OrganizingEventsService.Application.Contracts.Event;
using OrganizingEventsService.Application.Contracts.Feedback;
using OrganizingEventsService.Application.Contracts.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Abstractions.Services;

public abstract class EventService
{
    protected IAccountService AccountService { get; }
    
    protected EventService(IAccountService accountService)
    {
        AccountService = accountService;
    }

    public abstract EventDto GetEventInfo(Guid? eventPk, string? inviteCode);
    
    public abstract IEnumerable<EventDto> GetEventsWhereAccountIsParticipant(
        Guid accountPk,
        EventParticipantInviteStatus eventParticipant,
        PaginationDto paginationDto);
    
    public abstract NewEventDto CreateEvent(Guid organizerPk, CreateEventDto createEventDto);

    public abstract Guid PartiallyUpdateEvent(Guid eventPk, UpdateEventDto updateEventDto);

    public abstract void DeleteEventByPk(Guid eventPk);

    public abstract void UpdateParticipantStatus(
        Guid currentAccountPk,
        UpdateParticipantStatusDto updateInvitationStatusDto);

    public abstract IEnumerable<ParticipantDto> GetParticipants(Guid eventPk);

    public abstract ParticipantDto GetParticipantByPk(Guid participantPk);

    public abstract void CreateParticipants(IEnumerable<CreateParticipantDto> createParticipantDtos);

    public abstract ParticipantDto PartiallyUpdateParticipant(
        Guid participantPk,
        UpdateParticipantDto updateParticipantDto);

    public abstract void DeleteParticipantsByEmails(IEnumerable<string> accountEmails);
    
    public abstract void DeleteParticipantByAccountPk(Guid currentAccountPk);
    
    public abstract IEnumerable<FeedbackDto> GetFeedbacksByEventPk(Guid eventPk, Guid currentAccountPk);
    
    public abstract Guid CreateFeedback(Guid eventPk, Guid currentAccountPk, CreateFeedbackDto createFeedbackDto);

    public abstract FeedbackDto PartiallyUpdateFeedback(
        Guid feedbackPk,
        Guid currentAccountPk,
        UpdateFeedbackDto updateFeedbackDto);
    
    public abstract void DeleteFeedbackByPk(Guid feedbackPk, Guid currentAccountPk);
}