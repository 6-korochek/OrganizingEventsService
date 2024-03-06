using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Mapping;
using OrganizingEventsService.Infrastructure.Persistence.Models;
using OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public class AccountRepository : BaseRepository<Account, AccountModel>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Accounts) { }

    public IAsyncEnumerable<Account> GetListByQuery(AccountQuery query)
    {
        var queryAdapter = new AccountQueryToEfOrmAdapter(query, DbContext);
        return queryAdapter.Adapt().AsAsyncEnumerable().Select(AccountMapper.ToEntity);
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