using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }
    
    [Authorize("IsAuthenticated")]
    [HttpGet]
    public ActionResult<EventDto> Get(
        [FromQuery(Name = "event_id")] Guid? eventId,
        [FromQuery(Name = "invite_code")] string? inviteCode)
    {
        EventDto response = _eventService.GetEventInfo(eventId, inviteCode);
        return response;
    }
    
    [Authorize("IsAuthenticated")]
    [HttpPost]
    public ActionResult<NewEventDto> Create([FromBody] CreateEventDto createEventDto)
    {
        var currentAccount = HttpContext.Items["CurrentAccount"] as AuthenticatedAccountDto;
        NewEventDto response = _eventService.CreateEvent(currentAccount!.Account.Id, createEventDto);
        return response;
    }
    
    // Only Organizers
    [HttpPatch("{id}")]
    public ActionResult<Guid> PartiallyUpdate(Guid id,[FromBody] UpdateEventDto updateEventDto)
    {
        Guid response = _eventService.PartiallyUpdateEvent(id, updateEventDto);
        return response;
    }
    
    [Authorize("IsAuthenticated")]
    [Authorize("IsOrganizer")]
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        _eventService.DeleteEventById(id);
    }
    
    [Authorize("IsAuthenticated")]
    [HttpGet("/my")]
    public IEnumerable<EventDto> GetMy(
        [FromQuery(Name = "event_status")] EventStatus status,
        [FromQuery(Name = "limit")] uint? limit,
        [FromQuery(Name = "offset")] uint? offset,
        AuthenticatedAccountDto authenticatedAccountDto)
    {
        PaginationDto paginationDto = new PaginationDto
        {
            Limit = limit,
            Offset = offset
        };

        IEnumerable<EventDto> response = _eventService.GetEventsWhereAccountIsParticipant(
            authenticatedAccountDto.Account.Id, 
            status, 
            paginationDto);

        return response;
    }
    
    // Only Organizers
    [HttpGet("{id}/members")]
    public IEnumerable<ParticipantDto> GetMembers(Guid id)
    {
        IEnumerable<ParticipantDto> response = _eventService.GetParticipants(id);
        return response;
    }
    
    // Only Organizers
    [HttpPost("{id}/members")]
    public void CreateMembers(Guid id, [FromBody] IEnumerable<CreateParticipantDto> createParticipantDtoList)
    {
        IEnumerable<CreateParticipantDto> participantList = createParticipantDtoList.ToList();
        _eventService.CreateParticipants(id, participantList);
    }
    
    // Only Organizers
    [HttpDelete("{id}/members")]
    public void DeleteMembers(Guid id, [FromBody] IEnumerable<string> accountEmails)
    {
        _eventService.DeleteParticipantsByEmails(accountEmails);
    }
    
    // ?????????????????????????????????????????????????????
    // Only Organizers
    [HttpGet($"{{id}}/members/{{accountId}}")]
    public ActionResult<ParticipantDto> GetMember(Guid accountId)
    {
        var response = _eventService.GetParticipantByAccountId(accountId);
        return response;
    }
    
    // ?????????????????????????????????????????????????????
    // Only Organizers
    [HttpPatch($"{{id}}/members/{{accountId}}")]
    public ActionResult<ParticipantDto> PartiallyUpdateMember(Guid accountId, [FromBody] UpdateParticipantDto updateParticipantDto)
    {
        var response = _eventService.PartiallyUpdateParticipant(accountId, updateParticipantDto);
        return response;
    }
    
    // ?????????????????????????????????????????????????????
    // Only Organizers
    [HttpDelete($"{{id}}/members/{{accountId}}")]
    public void DeleteMember(Guid accountId)
    {
        _eventService.DeleteParticipantByAccountId(accountId);
    }
    
    // Feedbacks
    ///////////////////////////////
}