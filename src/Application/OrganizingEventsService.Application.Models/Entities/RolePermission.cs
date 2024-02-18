namespace OrganizingEventsService.Application.Models.Entities;

public partial class RolePermission
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public virtual Permission PermissionIdNavigation { get; set; } = null!;

    public virtual Role RoleIdNavigation { get; set; } = null!;
}
