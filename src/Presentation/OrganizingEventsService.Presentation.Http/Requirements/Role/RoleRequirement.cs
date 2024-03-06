namespace OrganizingEventsService.Presentation.Http.Requirements.Role;

public class RoleRequirement: BaseRequirement
{
    public RoleRequirement(string roleName) : base($"Is{roleName}") {}
}