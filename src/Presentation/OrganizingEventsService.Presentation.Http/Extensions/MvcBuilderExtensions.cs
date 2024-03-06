using Microsoft.AspNetCore.Authorization;
using OrganizingEventsService.Presentation.Http.Middlewares;
using OrganizingEventsService.Presentation.Http.Requirements.Authentication;
using OrganizingEventsService.Presentation.Http.Requirements.Role;

namespace OrganizingEventsService.Presentation.Http.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPresentationHttp(this IMvcBuilder builder)
    {
        builder.Services.AddTransient<ExceptionHandlerMiddleware>();
        builder.Services.AddAuthorization(options =>
        {
            AuthenticationRequirement authenticationRequirement = new AuthenticationRequirement();
            RoleRequirement roleRequirement = new RoleRequirement("Organizer");
            
            options.AddPolicy(authenticationRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(authenticationRequirement));
            
            options.AddPolicy(roleRequirement.RequirementName,
                policyBuilder => policyBuilder.Requirements.Add(roleRequirement));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, AuthenticationRequirementHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
        
        return builder.AddApplicationPart(typeof(IAssemblyMarker).Assembly);
    }
}