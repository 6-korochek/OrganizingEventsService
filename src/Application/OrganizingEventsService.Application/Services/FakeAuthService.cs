using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Services;

public class FakeAuthService : AuthService
{
    public FakeAuthService(IAccountService accountService) : base(accountService) {}

    public override AuthenticatedAccountDto AuthenticateAccount(string token)
    {
        return new AuthenticatedAccountDto
        {
            Token = "Boss Of this Gym!",
            Account = new AccountDto
            {
                Id = new Guid(),
                Name = "Andrey",
                Surname = "Hard",
                Email = "Фиг вам",
                IsInvite = true
            }
        };
    }

    public override AuthenticatedAccountDto Register(RegisterAccountDto registerAccountDto)
    {
        return new AuthenticatedAccountDto
        {
            Token = "some jwt string",
            Account = new AccountDto
            {
                Id = new Guid(),
                Name = "Andrey",
                Surname = "Hard",
                Email = "Фиг вам",
                IsInvite = true
            }
        };
    }

    public override AuthenticatedAccountDto Login(LoginAccountDto loginAccountDto)
    {
        return new AuthenticatedAccountDto
        {
            Token = "some jwt string",
            Account = new AccountDto
            {
                Id = new Guid(),
                Name = "Andrey",
                Surname = "Hard",
                Email = "Фиг вам",
                IsInvite = true
            }
        };
    }

    public override void Logout(string token) {}
}