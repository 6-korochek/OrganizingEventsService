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

    public abstract Task<EventDto> GetEventInfo(Guid? eventId, string? inviteCode);

    public abstract IAsyncEnumerable<EventDto> GetEventsWhereAccountIsParticipant(
        Guid accountId,
        EventStatus status,
        PaginationDto? paginationDto);

    public abstract Task<NewEventDto> CreateEvent(Guid organizerId, CreateEventDto createEventDto);

    public abstract Task<Guid> PartiallyUpdateEvent(Guid eventId, UpdateEventDto updateEventDto);

    public abstract Task DeleteEventById(Guid eventId);

    public abstract IAsyncEnumerable<ParticipantDto> GetParticipants(Guid eventId);

    public abstract Task<ParticipantDto> GetParticipantInEvent(Guid eventId, Guid accountId);

    public abstract void CreateParticipants(Guid eventId, IEnumerable<CreateParticipantDto> createParticipantDtoList);

    public abstract Task<ParticipantDto> PartiallyUpdateParticipant(
        Guid eventId,
        Guid accountId,
        UpdateParticipantDto updateParticipantDto);

    public abstract void DeleteParticipantsByEmails(IEnumerable<string> accountEmails);

    public abstract void DeleteParticipantByAccountId(Guid accountId);

    public abstract FeedbackDto GetFeedbackInfo(Guid feedbackId);

    public abstract IEnumerable<FeedbackDto> GetFeedbacksByEventId(Guid eventId);

    public abstract Guid CreateFeedback(Guid eventId, CreateFeedbackDto createFeedbackDto);

    public abstract FeedbackDto PartiallyUpdateFeedback(Guid feedbackId, UpdateFeedbackDto updateFeedbackDto);

    public abstract void DeleteFeedbackById(Guid feedbackId);
}