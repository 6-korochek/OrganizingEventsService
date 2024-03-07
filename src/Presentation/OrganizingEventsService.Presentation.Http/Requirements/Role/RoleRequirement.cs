namespace OrganizingEventsService.Presentation.Http.Requirements.Role;

public class RoleRequirement: BaseRequirement
{
    public string RoleName { get; }
    
    public RoleRequirement(string roleName) : base($"Is{roleName}") {
        RoleName = roleName;
    }
}