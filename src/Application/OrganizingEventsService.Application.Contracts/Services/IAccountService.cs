using OrganizingEventsService.Application.Contracts.Account;

namespace OrganizingEventsService.Application.Contracts.Services;

public interface IAccountService
{
    AccountDto GetAccountById(Guid accountId);

    AccountDto GetAccountByEmail(string email); 
    
    AccountDto UpdateAccount(Guid accountId, UpdateAccountDto updateAccountDto);
    
    void DeleteAccountById(Guid accountId);
}