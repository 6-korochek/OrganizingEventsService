using Microsoft.Extensions.DependencyInjection;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Services;

namespace OrganizingEventsService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAccountService, AccountServiceImpl>();
        collection.AddScoped<AuthService, AuthServiceImpl>();
        collection.AddScoped<EventService, EventServiceImpl>();
        
        // Register CQRS Mediator + Handlers
        collection.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        return collection;
    }
}