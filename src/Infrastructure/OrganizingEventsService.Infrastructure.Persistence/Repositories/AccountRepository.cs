using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Mapping;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public class AccountRepository : BaseRepository<Account, AccountModel>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Accounts)
    { }
    
    public IAsyncEnumerable<Account> GetListByQuery(AccountQuery query)
    {
        IQueryable<AccountModel> queryable = DbContext.Accounts;
        if (query.Ids.Any())
        {
            queryable = queryable.Where(model => query.Ids.Contains(model.Id));
        }

        if (query.Emails.Any())
        {
            queryable = queryable.Where(model => query.Emails.Contains(model.Email));
        }
        
        if (query.Offset is not null)
        {
            queryable = queryable.Skip((int)query.Offset);
        }

        if (query.Limit is not null)
        {
            queryable = queryable.Take((int)query.Limit);
        }
        
        return queryable.AsAsyncEnumerable().Select(AccountMapper.ToEntity);
    }

    protected override Account MapToEntity(AccountModel model)
    {
        return AccountMapper.ToEntity(model);
    }

    protected override AccountModel MapToModel(Account entity)
    {
        return AccountMapper.ToModel(entity);
    }
}