using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Feedback;

namespace OrganizingEventsService.Presentation.Http.Requirements.IsFeedbackAuthor;

public class FeedbackAuthorRequirementHandler : AuthorizationHandler<FeedbackAuthorRequirement>
{
    private readonly EventService _eventService;

    public FeedbackAuthorRequirementHandler(EventService eventService)
    {
        _eventService = eventService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FeedbackAuthorRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return Task.CompletedTask;
        if (httpContext.Items["CurrentAccount"] is not AuthenticatedAccountDto currentAccount)
        {
            throw new ForbiddenException();
        }

        Guid feedbackId = (Guid)httpContext.GetRouteValue("id")!;
        FeedbackDto feedback = _eventService.GetFeedbackInfo(feedbackId).Result;
        if (feedback.Author?.AccountId != currentAccount.Account.Id)
        {
            throw new ForbiddenException();
        }
        
        return Task.CompletedTask;
    }
}