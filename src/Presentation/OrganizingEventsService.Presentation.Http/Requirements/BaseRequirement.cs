using Microsoft.AspNetCore.Authorization;

namespace OrganizingEventsService.Presentation.Http.Requirements;

public abstract class BaseRequirement : IAuthorizationRequirement
{
    public string RequirementName { get; }

    protected BaseRequirement(string requirementName)
    {
        RequirementName = requirementName;
    }
}