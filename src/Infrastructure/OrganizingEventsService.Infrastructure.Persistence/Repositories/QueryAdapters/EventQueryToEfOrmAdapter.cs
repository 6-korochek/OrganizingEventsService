using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

public class EventQueryToEfOrmAdapter(EventQuery query, ApplicationDbContext dbContext) 
    : QueryToEfOrmAdapter<EventQuery, EventModel>(query, dbContext)
{
    public override IQueryable<EventModel> Adapt()
    {
        IQueryable<EventModel> queryable = DbContext.Events;
        if (query.Ids.Any())
        {
            queryable = queryable.Where(model => query.Ids.Contains(model.Id));
        }

        if (query.Statuses.Any())
        {
            queryable = queryable.Where(model => query.Statuses.Contains((EventStatus)model.Status));
        }

        if (query.IncludeParticipants)
        {
            queryable = queryable.Include(model => model.EventParticipants);
        }

        if (query.Offset is not null)
        {
            queryable = queryable.Skip((int)query.Offset);
        }

        if (query.Limit is not null)
        {
            queryable = queryable.Take((int)query.Limit);
        }

        return queryable;
    }
}