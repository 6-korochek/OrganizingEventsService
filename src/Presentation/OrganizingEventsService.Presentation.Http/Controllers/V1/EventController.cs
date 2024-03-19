using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Presentation.Http.Controllers.V1;

[Authorize("IsAuthenticated")]
[Route("/api/v1/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }
    
    [Authorize("IsInvited")]
    [HttpGet]
    public async Task<ActionResult<EventDto>> Get(
        [FromQuery(Name = "event_id")] Guid? eventId,
        [FromQuery(Name = "invite_code")] string? inviteCode)
    {
        EventDto response = await _eventService.GetEventInfo(eventId, inviteCode);
        return response;
    }
    
    [HttpPost]
    public async Task<ActionResult<NewEventDto>> Create([FromBody] CreateEventDto createEventDto)
    {
        var currentAccount = HttpContext.Items["CurrentAccount"] as AuthenticatedAccountDto;
        NewEventDto response = await _eventService.CreateEvent(currentAccount!.Account.Id, createEventDto);
        return response;
    }
    
    [Authorize("IsOrganizer")]
    [HttpPatch("{eventId}")]
    public async Task<ActionResult<Guid>> PartiallyUpdate(Guid eventId,[FromBody] UpdateEventDto updateEventDto)
    {
        Guid response = await _eventService.PartiallyUpdateEvent(eventId, updateEventDto);
        return response;
    }
    
    [Authorize("IsOrganizer")]
    [HttpDelete("{eventId}")]
    public void Delete(Guid eventId)
    {
        _eventService.DeleteEventById(eventId);
    }
    
    [HttpGet("/my")]
    public IAsyncEnumerable<EventDto> GetMy(
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
        IAsyncEnumerable<EventDto> response = _eventService.GetEventsWhereAccountIsParticipant(
            currentAccount!.Account.Id,
            status,
            paginationDto);

        return response;
    }
    
    [Authorize("IsOrganizer")]
    [HttpGet("{eventId}/members")]
    public IAsyncEnumerable<ParticipantDto> GetMembers(Guid eventId)
    {
        IAsyncEnumerable<ParticipantDto> response = _eventService.GetParticipants(eventId);
        return response;
    }
    
    [Authorize("IsOrganizer")]
    [HttpPost("{eventId}/members")]
    public void CreateMembers(Guid eventId, [FromBody] IEnumerable<CreateParticipantDto> createParticipantDtoList)
    {
        IEnumerable<CreateParticipantDto> participantList = createParticipantDtoList.ToList();
        _eventService.CreateParticipants(eventId, participantList);
    }
    
    [Authorize("IsOrganizer")]
    [HttpDelete("{eventId}/members")]
    public void DeleteMembers(Guid eventId, [FromBody] IEnumerable<string> accountEmails)
    {
        _eventService.DeleteParticipantsByEmails(eventId, accountEmails);
    }
    
    [Authorize("IsOrganizer")]
    [HttpGet($"{{eventId}}/members/{{accountId}}")]
    public ActionResult<ParticipantDto> GetMember(Guid eventId, Guid accountId)
    {
        var response = _eventService.GetParticipantInEvent(eventId, accountId);
        return response.Result;
    }
    
    [Authorize("IsOrganizer")]
    [HttpPatch($"{{eventId}}/members/{{accountId}}")]
    public ActionResult<ParticipantDto> PartiallyUpdateMember(Guid eventId, Guid accountId, [FromBody] UpdateParticipantDto updateParticipantDto)
    {
        var response = _eventService.PartiallyUpdateParticipant(eventId, accountId, updateParticipantDto);
        return response.Result;
    }
    
    [Authorize("IsOrganizer")]
    [HttpDelete($"{{eventId}}/members/{{accountId}}")]
    public void DeleteMember(Guid eventId, Guid accountId)
    {
        _eventService.DeleteParticipantByAccountId(eventId, accountId);
    }
    
    [HttpGet("{eventId}/feedbacks")]
    public IEnumerable<FeedbackDto> GetFeedbacks(Guid eventId)
    {
        var response = _eventService.GetFeedbacksByEventId(eventId).Result;
        return response;
    }
    
    [Authorize("IsParticipant")]
    [HttpPost("{eventId}/feedbacks")]
    public ActionResult<Guid> CreateFeedback(Guid eventId, CreateFeedbackDto createFeedbackDto)
    {
        Guid response = _eventService.CreateFeedback(eventId, createFeedbackDto);
        return response;
    }
    
    [Authorize("IsParticipant")]
    [Authorize("IsFeedbackAuthor")]
    [HttpPatch($"{{eventId}}/feedback/{{feedbackId}}")]
    public ActionResult<FeedbackDto> PartiallyUpdateFeedback(Guid feedbackId, UpdateFeedbackDto updateFeedbackDto)
    {
        var response = _eventService.PartiallyUpdateFeedback(feedbackId, updateFeedbackDto).Result;
        return response;
    }
}