namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class RolePermissionModel
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public virtual PermissionModel PermissionIdNavigation { get; set; } = null!;

    public virtual RoleModel RoleIdNavigation { get; set; } = null!;
}
