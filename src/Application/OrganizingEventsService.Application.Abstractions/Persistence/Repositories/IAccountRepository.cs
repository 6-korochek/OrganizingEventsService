using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    IAsyncEnumerable<Account> GetListByQuery(AccountQuery query);
}