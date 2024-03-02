namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class RoleModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventParticipantModel> EventParticipants { get;  } = new List<EventParticipantModel>();

    public virtual ICollection<RolePermissionModel> RolePermissions { get;  } = new List<RolePermissionModel>();
}
