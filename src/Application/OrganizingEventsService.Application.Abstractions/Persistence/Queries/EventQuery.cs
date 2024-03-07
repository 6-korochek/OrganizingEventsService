using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Collections.ObjectModel;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

public class EventQuery
{
    public Collection<Guid> Ids { get; } = new();
    
    public Collection<EventStatus> Statuses { get; } = new();

    public Collection<Guid> AccountParticipantIds { get; } = new();
    
    public ushort? Limit { get; private set; }
    
    public ushort? Offset { get; private set; }
    
    public bool IncludeParticipants { get; private set; }
    
    public EventQuery WithId(Guid id)
    {
        Ids.Add(id);
        return this;
    }
    
    public EventQuery WithStatus(EventStatus status)
    {
        Statuses.Add(status);
        return this;
    }

    public EventQuery WithAccountParticipantId(Guid accountParticipantId)
    {
        AccountParticipantIds.Add(accountParticipantId);
        return this;
    }
    
    public EventQuery WithLimit(ushort limit)
    {
        Limit = limit;
        return this;
    }
    
    public EventQuery WithOffset(ushort offset)
    {
        Offset = offset;
        return this;
    }

    public EventQuery WithParticipants(bool includeParticipants = true)
    {
        IncludeParticipants = includeParticipants;
        return this;
    }
}