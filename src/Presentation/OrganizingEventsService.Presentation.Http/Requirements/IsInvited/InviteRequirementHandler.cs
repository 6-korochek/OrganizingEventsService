using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Requirements.IsInvited;

public class InviteRequirementHandler : AuthorizationHandler<InviteRequirement>
{
    private readonly EventService _eventService;

    public InviteRequirementHandler(EventService eventService)
    {
        _eventService = eventService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, InviteRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return;
        if (httpContext.Items["CurrentAccount"] is not AuthenticatedAccountDto currentAccount)
        {
            throw new ForbiddenException();
        }
        
        string? inviteCode = httpContext.Request.Query["inviteCode"].FirstOrDefault();
        if (inviteCode is not null)
        {
            context.Succeed(requirement);
        }

        string? eventId = httpContext.Request.Query["eventId"].FirstOrDefault();
        if (eventId is null)
        {
            throw new BadRequestException("Invalid eventId and inviteCode!");
        }
        
        // Throw Exception if does not exists
        await _eventService.GetParticipantInEvent(Guid.Parse(eventId), currentAccount.Account.Id);
        context.Succeed(requirement);
    }
}