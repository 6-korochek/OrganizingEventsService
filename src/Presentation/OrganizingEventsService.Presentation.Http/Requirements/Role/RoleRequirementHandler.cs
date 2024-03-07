using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Abstractions.Exceptions;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Participant;

namespace OrganizingEventsService.Presentation.Http.Requirements.Role;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly EventService _eventService;
    
    public RoleRequirementHandler(EventService eventService)
    {
        _eventService = eventService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return Task.CompletedTask;
        if (httpContext.Items["CurrentAccount"] is not AuthenticatedAccountDto currentAccount)
        {
            throw new ForbiddenException();
        }

        Guid eventId = (Guid)httpContext.GetRouteValue("id")!;
        ParticipantDto participant = 
            _eventService.GetParticipantInEvent(currentAccount.Account.Id, eventId);

        if (participant.Role.Name != requirement.RoleName)
        {
            throw new ForbiddenException();
        }
        
        return Task.CompletedTask;
    }
}