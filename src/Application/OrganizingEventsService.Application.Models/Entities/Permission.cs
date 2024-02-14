namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class Permission
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get;  } = new List<RolePermission>();
}
