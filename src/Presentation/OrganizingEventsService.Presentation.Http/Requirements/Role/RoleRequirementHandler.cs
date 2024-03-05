using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

namespace OrganizingEventsService.Presentation.Http.Requirements.Role;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IRoleRepository _roleRepository;
    
    public RoleRequirementHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        throw new NotImplementedException();
    }
}