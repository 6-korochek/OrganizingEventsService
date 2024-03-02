namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class PermissionModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RolePermissionModel> RolePermissions { get;  } = new List<RolePermissionModel>();
}
