using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    [HttpGet("{id}")]
    public IAsyncEnumerable<Event> Get(Guid id)
    {
        var query = new EventQuery().WithId(id).WithParticipants();
        var eventEntities = _eventRepository.GetListByQuery(query);
        return eventEntities;
    }

    [HttpPost("")]
    public async Task<ActionResult<Event>> Create()
    {
        var eventEntity= new Event
        {
            Id = new Guid(),
            Name = "Gatchi Party",
            StartDatetime = new DateTime(),
            Status = EventStatus.Planed,
            MaxParticipant = 10
        };
        
        await _eventRepository.Add(eventEntity);
        return eventEntity;
    }
    
    [HttpPost("{eventId}")]
    public async Task<ActionResult<string>> AddParticipants(Guid eventId)
    {
        var participants = new List<EventParticipant>();
        var participant1 = new EventParticipant
        {
            Id = new Guid(),
            AccountId = eventId,
            EventId = eventId,
            InviteStatus = EventParticipantInviteStatus.Pending,
            RoleId = eventId
        };
        
        participants.Add(participant1);
        await _eventRepository.AddParticipants(eventId, participants);
        return "Ok";
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Event>> Update(Guid id)
    {
        var eventEntity = await _eventRepository.GetById(id);
        eventEntity.InviteCode = "WOOP";
        await _eventRepository.Update(eventEntity);
        
        return eventEntity;
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(Guid id)
    {
        await _eventRepository.DeleteById(id);
        return "Ok";
    }
}