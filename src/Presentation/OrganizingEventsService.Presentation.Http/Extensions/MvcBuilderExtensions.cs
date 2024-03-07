using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Application.ApplicationConstants;
using OrganizingEventsService.Presentation.Http.Middlewares;
using OrganizingEventsService.Presentation.Http.Requirements.IsAuthenticated;
using OrganizingEventsService.Presentation.Http.Requirements.IsHasRole;

namespace OrganizingEventsService.Presentation.Http.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPresentationHttp(this IMvcBuilder builder)
    {
        builder.Services.AddTransient<ExceptionHandlerMiddleware>();
        builder.Services.AddAuthorization(options =>
        {
            AuthenticationRequirement authenticationRequirement = new AuthenticationRequirement();
            RoleRequirement isOrganizerRequirement = new RoleRequirement(Roles.ORGANIZER, "IsOrganizer");
            RoleRequirement isParticipantRequirement = new RoleRequirement(Roles.PARTICIPANT,"IsParticipant");
            
            options.AddPolicy(authenticationRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(authenticationRequirement));
            
            options.AddPolicy(isOrganizerRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isOrganizerRequirement));
            
            options.AddPolicy(isParticipantRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(isParticipantRequirement));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, AuthenticationRequirementHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
        
        return builder.AddApplicationPart(typeof(IAssemblyMarker).Assembly);
    }
}