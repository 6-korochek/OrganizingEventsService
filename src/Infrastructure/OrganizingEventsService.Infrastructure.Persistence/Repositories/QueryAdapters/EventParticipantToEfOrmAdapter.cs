using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

public class EventParticipantToEfOrmAdapter(EventParticipantQuery query, ApplicationDbContext dbContext) : QueryToEfOrmAdapter<EventParticipantQuery, EventParticipantModel>(query, dbContext)
{
    public override IQueryable<EventParticipantModel> Adapt()
    {
        IQueryable<EventParticipantModel> queryable = DbContext.EventParticipants;
        if (query.Ids.Any())
        {
            queryable = queryable.Where(model => query.Ids.Contains(model.Id));
        }

        if (query.EventIds.Any())
        {
            queryable = queryable.Where(model => query.EventIds.Contains(model.EventId));
        }

        if (query.AccountIds.Any())
        {
            queryable = queryable.Where(model => query.AccountIds.Contains(model.AccountId));
        }

        if (query.InviteStatuses.Any())
        {
            queryable = queryable.Where(model =>
                query.InviteStatuses.Contains((EventParticipantInviteStatus)model.InviteStatus));
        }

        if (query.IsBanned)
        {
            queryable = queryable.Include(model => model.IsBanned);
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