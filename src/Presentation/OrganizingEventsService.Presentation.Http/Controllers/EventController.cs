using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[Authorize("IsAuthenticated")]
[Route("[controller]")]
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
    [HttpPatch("{id}")]
    public async Task<ActionResult<Guid>> PartiallyUpdate(Guid id,[FromBody] UpdateEventDto updateEventDto)
    {
        Guid response = await _eventService.PartiallyUpdateEvent(id, updateEventDto);
        return response;
    }
    
    [Authorize("IsOrganizer")]
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        _eventService.DeleteEventById(id);
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
    [HttpGet("{id}/members")]
    public IAsyncEnumerable<ParticipantDto> GetMembers(Guid id)
    {
        IAsyncEnumerable<ParticipantDto> response = _eventService.GetParticipants(id);
        return response;
    }
    
    [Authorize("IsOrganizer")]
    [HttpPost("{id}/members")]
    public void CreateMembers(Guid id, [FromBody] IEnumerable<CreateParticipantDto> createParticipantDtoList)
    {
        IEnumerable<CreateParticipantDto> participantList = createParticipantDtoList.ToList();
        _eventService.CreateParticipants(id, participantList);
    }
    
    [Authorize("IsOrganizer")]
    [HttpDelete("{id}/members")]
    public void DeleteMembers(Guid id, [FromBody] IEnumerable<string> accountEmails)
    {
        _eventService.DeleteParticipantsByEmails(accountEmails);
    }
    
    [Authorize("IsOrganizer")]
    [HttpGet($"{{id}}/members/{{accountId}}")]
    public ActionResult<ParticipantDto> GetMember(Guid id, Guid accountId)
    {
        var response = _eventService.GetParticipantInEvent(id, accountId);
        return response.Result;
    }
    
    [Authorize("IsOrganizer")]
    [HttpPatch($"{{id}}/members/{{accountId}}")]
    public ActionResult<ParticipantDto> PartiallyUpdateMember(Guid id, Guid accountId, [FromBody] UpdateParticipantDto updateParticipantDto)
    {
        var response = _eventService.PartiallyUpdateParticipant(id, accountId, updateParticipantDto);
        return response.Result;
    }
    
    [Authorize("IsOrganizer")]
    [HttpDelete($"{{id}}/members/{{accountId}}")]
    public void DeleteMember(Guid accountId)
    {
        _eventService.DeleteParticipantByAccountId(accountId);
    }
    
    [HttpGet("{id}/feedbacks")]
    public IEnumerable<FeedbackDto> GetFeedbacks(Guid id)
    {
        IEnumerable<FeedbackDto> response = _eventService.GetFeedbacksByEventId(id);
        return response;
    }
    
    [Authorize("IsParticipant")]
    [HttpPost("{id}/feedbacks")]
    public ActionResult<Guid> CreateFeedback(Guid id, CreateFeedbackDto createFeedbackDto)
    {
        Guid response = _eventService.CreateFeedback(id, createFeedbackDto);
        return response;
    }
    
    [Authorize("IsParticipant")]
    [Authorize("IsFeedbackAuthor")]
    [HttpPatch("{id}/feedbacks")]
    public ActionResult<FeedbackDto> PartiallyUpdateFeedback(Guid id, UpdateFeedbackDto updateFeedbackDto)
    {
        FeedbackDto response = _eventService.PartiallyUpdateFeedback(id, updateFeedbackDto);
        return response;
    }
}