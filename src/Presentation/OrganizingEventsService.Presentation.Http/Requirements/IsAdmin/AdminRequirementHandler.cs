using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Requirements.IsAdmin;

public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return Task.CompletedTask;
        if (httpContext.Items["CurrentAccount"] is not AuthenticatedAccountDto currentAccount)
        {
            throw new ForbiddenException();
        }

        if (!currentAccount.Account.IsAdmin)
        {
            throw new ForbiddenException();
        }
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}