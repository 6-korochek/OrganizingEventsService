using OrganizingEventsService.Application.Contracts.Account;
using OrganizingEventsService.Application.Contracts.Event;
using OrganizingEventsService.Application.Contracts.Invitation;
using OrganizingEventsService.Infrastructure.Persistence.Entities;

namespace OrganizingEventsService.Application.Abstractions.Services;

public interface IAccountService
{
    public abstract AccountDto GetAccountByPk(Guid accountPk);
    
    public abstract AccountDto UpdateAccount(Guid accountPk, UpdateAccountDto updateAccountDto);
    
    public abstract void DeleteAccountById(Guid accountPk);
}