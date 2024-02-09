using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;

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
        return collection;
    }
}