namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class RolePermission
{
    public Guid Id { get; set; }

    public Guid RolePk { get; set; }

    public Guid PermissionPk { get; set; }

    public virtual Permission PermissionPkNavigation { get; set; } = null!;

    public virtual Role RolePkNavigation { get; set; } = null!;
}
