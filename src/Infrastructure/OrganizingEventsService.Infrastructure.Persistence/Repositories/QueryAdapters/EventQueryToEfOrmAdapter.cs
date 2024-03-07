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
        if (Query.Ids.Any())
        {
            queryable = queryable.Where(model => Query.Ids.Contains(model.Id));
        }

        if (Query.Statuses.Any())
        {
            queryable = queryable.Where(model => Query.Statuses.Contains((EventStatus)model.Status));
        }

        if (Query.AccountParticipantIds.Any())
        {
            queryable = queryable
                .Where(model =>
                    model.EventParticipants
                        .Select(p => p.AccountId)
                        .Any(p => Query.AccountParticipantIds.Contains(p)
                        ));
        }

        if (Query.Offset is not null)
        {
            queryable = queryable.Skip((int)Query.Offset);
        }

        if (Query.Limit is not null)
        {
            queryable = queryable.Take((int)Query.Limit);
        }
        
        if (Query.IncludeParticipants)
        {
            queryable = queryable.Include(model => model.EventParticipants);
        }

        return queryable;
    }
}