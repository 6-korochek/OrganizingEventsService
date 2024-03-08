namespace OrganizingEventsService.Presentation.Http.Requirements.IsHasRole;

public class RoleRequirement: BaseRequirement
{
    public Guid RoleId { get; }
    
    public RoleRequirement(Guid roleId, string requirementName) : base(requirementName)
    {
        RoleId = roleId;
    }
}