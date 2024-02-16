namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class Role
{
    public Guid Pk { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventParticipant> EventParticipants { get;  } = new List<EventParticipant>();

    public virtual ICollection<RolePermission> RolePermissions { get;  } = new List<RolePermission>();
}
