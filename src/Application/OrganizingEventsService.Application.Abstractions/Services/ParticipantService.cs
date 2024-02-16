using OrganizingEventsService.Application.Contracts.Common;
using OrganizingEventsService.Application.Contracts.Event;
using OrganizingEventsService.Application.Contracts.Feedback;
using OrganizingEventsService.Application.Contracts.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Abstractions.Services;

public abstract class ParticipantService
{
    protected IAccountService AccountService { get; }
    protected IEventService EventService { get; }
    
    protected ParticipantService(IAccountService accountService, IEventService eventService)
    {
        AccountService = accountService;
        EventService = eventService;
    }
    
    public abstract IEnumerable<EventDto> GetEventsWhereAccountIsParticipant(
        Guid accountPk,
        EventStatus eventStatus,
        PaginationDto paginationDto);
    
    public abstract void UpdateParticipantStatus(Guid accountPk, UpdateParticipantStatusDto updateInvitationStatusDto);
    
    public abstract IEnumerable<ParticipantDto> GetEventParticipants(Guid eventPk);

    public abstract ParticipantDto GetParticipantByPk(Guid participantPk);

    public abstract void CreateParticipants(IEnumerable<CreateParticipantDto> createParticipantDtos);
    
    public abstract ParticipantDto PartiallyUpdateParticipant(Guid participantPk, UpdateParticipantDto updateParticipantDto);

    public abstract void DeleteParticipantsByEmails(IEnumerable<string> accountEmails);
    
    public abstract void DeleteParticipantByPk(Guid participantPk);
    
    public abstract IEnumerable<FeedbackDto> GetFeedbacksByEventPk(Guid eventPk, Guid currentAccountPk);

    public abstract Guid CreateFeedback(Guid eventPk, Guid currentAccountPk, CreateFeedbackDto createFeedbackDto);

    public abstract FeedbackDto PartiallyUpdateFeedback(
        Guid feedbackPk,
        Guid currentAccountPk,
        UpdateFeedbackDto updateFeedbackDto);

    public abstract void DeleteFeedbackByPk(Guid feedbackPk, Guid currentAccountPk);
}