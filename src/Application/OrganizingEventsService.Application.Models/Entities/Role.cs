namespace OrganizingEventsService.Application.Models.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventParticipant> EventParticipants { get;  } = new List<EventParticipant>();

    public virtual ICollection<RolePermission> RolePermissions { get;  } = new List<RolePermission>();
}
