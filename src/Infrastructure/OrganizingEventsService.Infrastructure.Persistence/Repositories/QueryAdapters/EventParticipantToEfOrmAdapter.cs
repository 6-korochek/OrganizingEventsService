using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

public class EventParticipantToEfOrmAdapter(EventParticipantQuery query, ApplicationDbContext dbContext) 
    : QueryToEfOrmAdapter<EventParticipantQuery, EventParticipantModel>(query, dbContext)
{
    public override IQueryable<EventParticipantModel> Adapt()
    {
        IQueryable<EventParticipantModel> queryable = DbContext.EventParticipants;
        if (Query.Ids.Any())
        {
            queryable = queryable.Where(model => Query.Ids.Contains(model.Id));
        }

        if (Query.EventIds.Any())
        {
            queryable = queryable.Where(model => Query.EventIds.Contains(model.EventId));
        }

        if (Query.AccountIds.Any())
        {
            queryable = queryable.Where(model => Query.AccountIds.Contains(model.AccountId));
        }

        if (Query.RoleIds.Any())
        {
            queryable = queryable.Where(model => Query.RoleIds.Contains(model.RoleId));
        }

        if (Query.InviteStatuses.Any())
        {
            queryable = queryable.Where(model =>
                Query.InviteStatuses.Contains((EventParticipantInviteStatus)model.InviteStatus));
        }

        if (Query.IsBanned)
        {
            queryable = queryable.Include(model => model.IsBanned);
        }

        if (Query.Offset is not null)
        {
            queryable = queryable.Skip((int)Query.Offset);
        }

        if (Query.Limit is not null)
        {
            queryable = queryable.Take((int)Query.Limit);
        }
        
        if (Query.IncludeAccount)
        {
            queryable = queryable.Include(model => model.AccountIdNavigation);
        }

        if (Query.IncludeRole)
        {
            queryable = queryable.Include(model => model.RoleIdNavigation);
        }

        return queryable;
    }
}