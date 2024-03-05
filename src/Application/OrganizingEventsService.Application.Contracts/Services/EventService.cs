using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
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
        EventStatus status,
        PaginationDto paginationDto);

    public abstract NewEventDto CreateEvent(Guid organizerId, CreateEventDto createEventDto);

    public abstract Guid PartiallyUpdateEvent(Guid eventId, UpdateEventDto updateEventDto);

    public abstract void DeleteEventById(Guid eventId);

    public abstract void UpdateParticipantStatus(
        Guid currentAccountId,
        UpdateParticipantStatusDto updateInvitationStatusDto);

    public abstract IEnumerable<ParticipantDto> GetParticipants(Guid eventId);

    public abstract ParticipantDto GetParticipantByAccountId(Guid accountId);

    public abstract void CreateParticipants(Guid eventId, IEnumerable<CreateParticipantDto> createParticipantDtoList);

    public abstract ParticipantDto PartiallyUpdateParticipant(
        Guid accountId,
        UpdateParticipantDto updateParticipantDto);

    public abstract void DeleteParticipantsByEmails(IEnumerable<string> accountEmails);

    public abstract void DeleteParticipantByAccountId(Guid accountId);

    public abstract IEnumerable<FeedbackDto> GetFeedbacksByEventId(Guid eventId);

    public abstract Guid CreateFeedback(Guid eventId, CreateFeedbackDto createFeedbackDto);

    public abstract FeedbackDto PartiallyUpdateFeedback(Guid feedbackId, UpdateFeedbackDto updateFeedbackDto);

    public abstract void DeleteFeedbackById(Guid feedbackId);
}