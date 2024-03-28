using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.ApplicationConstants;
using OrganizingEventsService.Presentation.Http.Middlewares;
using OrganizingEventsService.Presentation.Http.Requirements.IsAdmin;
using OrganizingEventsService.Presentation.Http.Requirements.IsAuthenticated;
using OrganizingEventsService.Presentation.Http.Requirements.IsFeedbackAuthor;
using OrganizingEventsService.Presentation.Http.Requirements.IsHasRole;
using OrganizingEventsService.Presentation.Http.Requirements.IsInvited;

namespace OrganizingEventsService.Presentation.Http.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPresentationHttp(this IMvcBuilder builder)
    {
        builder.Services.AddSingleton<RequestLoggerMiddleware>();
        builder.Services.AddSingleton<ExceptionHandlerMiddleware>();
        
        builder.Services.AddAuthorization(options =>
        {
            AuthenticationRequirement isAuthenticatedRequirement = new AuthenticationRequirement();
            InviteRequirement isInvitedRequirement = new InviteRequirement();
            AdminRequirement isAdminRequirement = new AdminRequirement();
            RoleRequirement isOrganizerRequirement = new RoleRequirement(Roles.ORGANIZER, "IsOrganizer");
            RoleRequirement isParticipantRequirement = new RoleRequirement(Roles.PARTICIPANT,"IsParticipant");
            
            options.AddPolicy(isAuthenticatedRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isAuthenticatedRequirement));
            
            options.AddPolicy(isInvitedRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isInvitedRequirement));

            options.AddPolicy(isAdminRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isAdminRequirement));
            
            options.AddPolicy(isOrganizerRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isOrganizerRequirement));
            
            options.AddPolicy(isParticipantRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isParticipantRequirement));
        });

        builder.Services.AddTransient<IAuthorizationHandler, AuthenticationRequirementHandler>();
        builder.Services.AddTransient<IAuthorizationHandler, RoleRequirementHandler>();
        builder.Services.AddTransient<IAuthorizationHandler, FeedbackAuthorRequirementHandler>();
        builder.Services.AddTransient<IAuthorizationHandler, InviteRequirementHandler>();
        builder.Services.AddTransient<IAuthorizationHandler, AdminRequirementHandler>();

        return builder.AddApplicationPart(typeof(IAssemblyMarker).Assembly);
    }
}