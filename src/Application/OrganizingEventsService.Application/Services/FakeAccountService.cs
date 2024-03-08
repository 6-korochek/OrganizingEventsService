using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Services;

public class FakeAccountService : IAccountService
{
    public AccountDto GetAccountById(Guid accountId)
    {
        return new AccountDto
        {
            Id = new Guid(),
            Email = "Фиг вам",
            Name = "Andrey",
            Surname = "Hard",
            IsInvite = true
        };
    }

    public AccountDto GetAccountByEmail(string email)
    {
        return new AccountDto
        {
            Id = new Guid(),
            Email = "Фиг вам",
            Name = "Andrey",
            Surname = "Hard",
            IsInvite = true
        };
    }

    public AccountDto UpdateAccount(Guid accountId, UpdateAccountDto updateAccountDto)
    {
        return new AccountDto
        {
            Id = new Guid(),
            Email = "Фиг вам",
            Name = "Andrey",
            Surname = "Hard",
            IsInvite = true
        };
    }

    public void DeleteAccountById(Guid accountId) {}
    public AccountDto CreateAccount(CreateAccountDto createAccountDto)
    {
        throw new NotImplementedException();
    }
}