using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

public class AccountQueryToEfOrmAdapter(AccountQuery query, ApplicationDbContext dbContext) : QueryToEfOrmAdapter<AccountQuery, AccountModel>(query, dbContext)
{
    public override IQueryable<AccountModel> Adapt()
    {
        IQueryable<AccountModel> queryable = DbContext.Accounts;
        if (Query.Ids.Any())
        {
            queryable = queryable.Where(model => Query.Ids.Contains(model.Id));
        }

        if (Query.Emails.Any())
        {
            queryable = queryable.Where(model => Query.Emails.Contains(model.Email));
        }

        if (Query.Offset is not null)
        {
            queryable = queryable.Skip((int)Query.Offset);
        }

        if (Query.Limit is not null)
        {
            queryable = queryable.Take((int)Query.Limit);
        }

        return queryable;
    }
}