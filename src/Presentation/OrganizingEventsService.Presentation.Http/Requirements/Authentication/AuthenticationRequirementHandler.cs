using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Contracts.Services;

namespace OrganizingEventsService.Presentation.Http.Requirements.Authentication;

public class AuthenticationRequirementHandler : AuthorizationHandler<AuthenticationRequirement>
{
    private readonly AuthService _authService;

    public AuthenticationRequirementHandler(AuthService authService)
    {
        _authService = authService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticationRequirement requirement)
    {
        throw new NotImplementedException();
    }
}