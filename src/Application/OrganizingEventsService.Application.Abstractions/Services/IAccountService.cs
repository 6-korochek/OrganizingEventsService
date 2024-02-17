using OrganizingEventsService.Application.Contracts.Account;

namespace OrganizingEventsService.Application.Abstractions.Services;

public interface IAccountService
{
    AccountDto GetAccountByPk(Guid accountPk);

    AccountDto GetAccountByEmail(string email); 
    
    AccountDto UpdateAccount(Guid accountPk, UpdateAccountDto updateAccountDto);
    
    void DeleteAccountById(Guid accountPk);
}