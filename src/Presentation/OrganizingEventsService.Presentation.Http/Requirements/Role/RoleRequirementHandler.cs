using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Abstractions.Exceptions;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Dto.Account;

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
        if (context.Resource is not HttpContext httpContext) return Task.CompletedTask;
        if (httpContext.Items["CurrentAccount"] is not AuthenticatedAccountDto currentAccount)
        {
            throw new ForbiddenException();
        }
        
        Console.WriteLine(currentAccount.Token);
        return Task.CompletedTask;
    }
}