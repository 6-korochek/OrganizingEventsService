using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Collections.ObjectModel;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

public class EventParticipantQuery
{
    public Collection<Guid> Ids { get; } = new();
    
    public Collection<Guid> EventIds { get; } = new();
    
    public Collection<Guid> AccountIds { get; } = new();
    
    public Collection<EventParticipantInviteStatus> InviteStatuses { get; } = new();
    
    public bool IsBanned;
    
    public ushort? Limit;
    
    public ushort? Offset;
    
    public EventParticipantQuery WithId(Guid id)
    {
        Ids.Add(id);
        return this;
    }
    
    public EventParticipantQuery WithEventId(Guid id)
    {
        EventIds.Add(id);
        return this;
    }
    
    public EventParticipantQuery WithAccountId(Guid id)
    {
        AccountIds.Add(id);
        return this;
    }
    
    public EventParticipantQuery WithStatus(EventParticipantInviteStatus status)
    {
        InviteStatuses.Add(status);
        return this;
    }
    
    public EventParticipantQuery WithBanned(bool isBanned = true)
    {
        IsBanned = isBanned;
        return this;
    }
    
    public EventParticipantQuery WithLimit(ushort limit)
    {
        Limit = limit;
        return this;
    }
    
    public EventParticipantQuery WithOffset(ushort offset)
    {
        Offset = offset;
        return this;
    }
}