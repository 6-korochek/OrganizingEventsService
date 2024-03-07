using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Collections.ObjectModel;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

public class EventParticipantQuery
{
    public Collection<Guid> Ids { get; } = new();
    
    public Collection<Guid> EventIds { get; } = new();
    
    public Collection<Guid> AccountIds { get; } = new();
    
    public Collection<string> RoleNames { get; } = new();
    
    public Collection<EventParticipantInviteStatus> InviteStatuses { get; } = new();
    
    public bool IsBanned { get; private set; }
    
    public ushort? Limit { get; private set; }
    
    public ushort? Offset { get; private set; }
    
    public bool IncludeAccount { get; private set; }
    
    public bool IncludeRole { get; private set; }
    
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

    public EventParticipantQuery WithRoleName(string roleName)
    {
        RoleNames.Add(roleName);
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

    public EventParticipantQuery WithAccount(bool includeAccount = true)
    {
        IncludeAccount = includeAccount;
        return this;
    }

    public EventParticipantQuery WithRole(bool includeRole = true)
    {
        IncludeRole = includeRole;
        return this;
    }
}