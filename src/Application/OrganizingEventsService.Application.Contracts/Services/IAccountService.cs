using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Contracts.Services;

public interface IAccountService
{
    Task<AccountDto> GetAccountById(Guid accountId);

    Task<AccountDto> GetAccountByEmail(string email);

    Task<IEnumerable<AccountDto>> GetExistingAccountsByEmail(IEnumerable<string> emails);

    Task<AccountDto> UpdateAccount(Guid accountId, UpdateAccountDto updateAccountDto);

    void DeleteAccountById(Guid accountId);

    Task<AccountDto> CreateAccount(CreateAccountDto createAccountDto);
}