using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Requirements.IsAuthenticated;

public class AuthenticationRequirementHandler : AuthorizationHandler<AuthenticationRequirement>
{
    private readonly AuthService _authService;

    public AuthenticationRequirementHandler(AuthService authService)
    {
        _authService = authService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticationRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return Task.CompletedTask;;
        
        string? token = httpContext.Request.Headers[requirement.AuthenticationHeaderName].FirstOrDefault();
        if (token is null)
        {
            throw new UnauthorizedException();
        }

        AuthenticatedAccountDto authenticatedAccountDto = _authService.AuthenticateAccount(token);
        httpContext.Items["CurrentAccount"] = authenticatedAccountDto;
        context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}