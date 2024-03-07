using Microsoft.Extensions.DependencyInjection;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Services;

namespace OrganizingEventsService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAccountService, FakeAccountService>();
        collection.AddScoped<EventService, EventServiceImpl>();
        collection.AddScoped<AuthService, FakeAuthService>();
        
        return collection;
    }
}