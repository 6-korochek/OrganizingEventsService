using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Services;

public class FakeEventService : EventService
{
    public FakeEventService(IAccountService accountService) : base(accountService) {}

    public override EventDto GetEventInfo(Guid? eventId, string? inviteCode)
    {
        return new EventDto
        {
            Id = new Guid(),
            Name = "Andrey",
            Description = "",
            MeetingLink = "https://iscander.itmo",
        };
    }

    public override IEnumerable<EventDto> GetEventsWhereAccountIsParticipant(Guid accountId, EventStatus status, PaginationDto paginationDto)
    {
        return new List<EventDto>
        {
            new EventDto
            {
                Id = new Guid(),
                Name = "Andrey",
                Description = "",
                MeetingLink = "https://iscander.itmo",
            }
        };
    }

    public override NewEventDto CreateEvent(Guid organizerId, CreateEventDto createEventDto)
    {
        return new NewEventDto
        {
            Id = new Guid(),
            InviteCode = "123456"
        };
    }

    public override Guid PartiallyUpdateEvent(Guid eventId, UpdateEventDto updateEventDto)
    {
        return new Guid();
    }

    public override void DeleteEventById(Guid eventId) {}

    public override void UpdateParticipantStatus(Guid currentAccountId, UpdateParticipantStatusDto updateInvitationStatusDto) {}

    public override IEnumerable<ParticipantDto> GetParticipants(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public override ParticipantDto GetParticipantByAccountId(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public override void CreateParticipants(Guid eventId, IEnumerable<CreateParticipantDto> createParticipantDtoList)
    {
        throw new NotImplementedException();
    }

    public override ParticipantDto PartiallyUpdateParticipant(Guid accountId, UpdateParticipantDto updateParticipantDto)
    {
        throw new NotImplementedException();
    }

    public override void DeleteParticipantsByEmails(IEnumerable<string> accountEmails) {}

    public override void DeleteParticipantByAccountId(Guid accountId) {}

    public override IEnumerable<FeedbackDto> GetFeedbacksByEventId(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public override Guid CreateFeedback(Guid eventId, CreateFeedbackDto createFeedbackDto)
    {
        throw new NotImplementedException();
    }

    public override FeedbackDto PartiallyUpdateFeedback(Guid feedbackId, UpdateFeedbackDto updateFeedbackDto)
    {
        throw new NotImplementedException();
    }

    public override void DeleteFeedbackById(Guid feedbackId) {}
}