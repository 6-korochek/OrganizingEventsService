using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Repositories;

namespace OrganizingEventsService.Infrastructure.Persistence.Extensions;

// using Itmo.Dev.Platform.Postgres.Extensions;
// using Itmo.Dev.Platform.Postgres.Plugins;
// using OrganizingEventsService.Application.Abstractions.Persistence;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetSection("Infrastructure:Persistence:Postgres:ConnectionString").Value));

        collection.AddScoped<IAccountRepository, AccountRepository>();
        return collection;
    }
}