using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Collections.ObjectModel;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

public class EventQuery
{
    public Collection<Guid> Ids { get; } = new();
    
    public Collection<EventStatus> Statuses { get; } = new();
    
    public ushort? Limit;
    
    public ushort? Offset;
    
    public bool IncludeParticipants;
    
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