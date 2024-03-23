using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Presentation.Http.Controllers.V2;

[Authorize("IsAuthenticated")]
[Route("/api/v2/[controller]")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize("IsInvited")]
    [HttpGet]
    public async Task<ActionResult<EventDto>> Get(
        [FromQuery(Name = "eventId")] Guid? eventId,
        [FromQuery(Name = "inviteCode")] string? inviteCode)
    {
        EventDto response = await _mediator.Send(new GetEventInfoEventQuery
            { EventId = eventId, InviteCode = inviteCode });
        return response;
    }

    [HttpPost]
    public async Task<ActionResult<NewEventDto>> Create([FromBody] CreateEventDto createEventDto)
    {
        var currentAccount = HttpContext.Items["CurrentAccount"] as AuthenticatedAccountDto;
        NewEventDto response = await _mediator.Send(new CreateEventCommand
            {
                OrganizerId = currentAccount!.Account.Id,
                EventRequest = createEventDto
            }
        );

        return response;
    }

    [Authorize("IsOrganizer")]
    [HttpPatch("{eventId}")]
    public async Task<ActionResult<Guid>> PartiallyUpdate(Guid eventId, [FromBody] UpdateEventDto updateEventDto)
    {
        Guid response = await _mediator.Send(
            new PartiallyUpdateEventCommand
            {
                EventId = eventId,
                EventRequest = updateEventDto
            }
        );

        return response;
    }

    [Authorize("IsOrganizer")]
    [HttpDelete("{eventId}")]
    public async void Delete(Guid eventId)
    {
        await _mediator.Send(
            new DeleteEventCommand()
            {
                EventId = eventId
            }
        );
    }

    [HttpGet("/my")]
    public async Task<IAsyncEnumerable<EventDto>> GetMy(
        [FromQuery(Name = "event_status")] EventStatus status,
        [FromQuery(Name = "limit")] ushort? limit = null,
        [FromQuery(Name = "offset")] ushort? offset = null)
    {
        PaginationDto? paginationDto = limit != null
            ? new PaginationDto
            {
                Limit = (ushort)limit,
                Offset = offset ?? 0
            }
            : null;

        var currentAccount = HttpContext.Items["CurrentAccount"] as AuthenticatedAccountDto;

        var response = await _mediator.Send(new GetMyEventQuery()
        {
            Status = status,
            EventRequest = paginationDto,
            AccountId = currentAccount!.Account.Id
        });
        return response;
    }

    [Authorize("IsOrganizer")]
    [HttpGet("{eventId}/members")]
    public async Task<IAsyncEnumerable<ParticipantDto>> GetMembers(Guid eventId)
    {
        var response = await _mediator.Send(new GetMembersEventQuery()
        {
            EventId = eventId
        });
        return response;
    }

    [Authorize("IsOrganizer")]
    [HttpPost("{eventId}/members")]
    public async void CreateMembers(Guid eventId, [FromBody] IEnumerable<CreateParticipantDto> createParticipantDtoList)
    {
        var participantList = createParticipantDtoList.ToList();
        await _mediator.Send(
            new CreateMembersEventCommand
            {
                EventId = eventId,
                EventRequest = participantList
            }
        );
    }

    [Authorize("IsOrganizer")]
    [HttpDelete("{eventId}/members")]
    public async void DeleteMembers(Guid eventId, [FromBody] IEnumerable<string> accountEmails)
    {
        await _mediator.Send(
            new DeleteMembersEventCommand()
            {
                EventId = eventId,
                AccountEmails = accountEmails
            }
        );
    }

    [Authorize("IsOrganizer")]
    [HttpGet($"{{eventId}}/members/{{accountId}}")]
    public async Task<ActionResult<ParticipantDto>> GetMember(Guid eventId, Guid accountId)
    {

        var response = await _mediator.Send(
            new GetMemberEventQuery()
            {
                EventId = eventId,
                AccountId = accountId
            }
        );

        return response;
    }

    [Authorize("IsOrganizer")]
    [HttpPatch($"{{eventId}}/members/{{accountId}}")]
    public async Task<ActionResult<ParticipantDto>> PartiallyUpdateMember(
        Guid eventId,
        Guid accountId,
        [FromBody] UpdateParticipantDto updateParticipantDto)
    {

        var response = await _mediator.Send(new PartiallyUpdateMemberEventCommand()
        {
            EventId = eventId,
            AccountId = accountId,
            EventRequest = updateParticipantDto
        });
        return response;
    }

    [Authorize("IsOrganizer")]
    [HttpDelete($"{{eventId}}/members/{{accountId}}")]
    public async void DeleteMember(Guid eventId, Guid accountId)
    {
        await _mediator.Send(
            new DeleteMemberEventCommand()
            {
                EventId = eventId,
                AccountId = accountId
            }
        );
    }

    [HttpGet("{eventId}/feedbacks")]
    public async Task<IEnumerable<FeedbackDto>> GetFeedbacks(Guid eventId)
    {
        var response = await _mediator.Send(new GetFeedbacksEventQuery()
        {
            EventId = eventId
        });
        return response;
    }

    [Authorize("IsParticipant")]
    [HttpPost("{eventId}/feedbacks")]
    public async Task<ActionResult<Guid>> CreateFeedback(Guid eventId, CreateFeedbackDto createFeedbackDto)
    {
        var response = await _mediator.Send(
            new CreateFeedbackEventCommand
            {
                EventId = eventId,
                EventRequest = createFeedbackDto
            }
        );

        return response;
    }

    [Authorize("IsParticipant")]
    [Authorize("IsFeedbackAuthor")]
    [HttpPatch($"{{eventId}}/feedback/{{feedbackId}}")]
    public async Task<ActionResult<FeedbackDto>> PartiallyUpdateFeedback(Guid feedbackId, UpdateFeedbackDto updateFeedbackDto)
    {
        var response = await _mediator.Send(
            new PartiallyUpdateFeedbackEventCommand
            {
                FeedbackId = feedbackId,
                EventRequest = updateFeedbackDto
            }
        );

        return response;
    }
}