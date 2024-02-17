using OrganizingEventsService.Application.Contracts.Account;

namespace OrganizingEventsService.Application.Abstractions.Services;

public abstract class AuthService
{
    protected IAccountService AccountService { get; }

    protected AuthService(IAccountService accountService)
    {
        AccountService = accountService;
    }

    public abstract AuthenticatedAccountDto AuthenticateAccountByToken(string token);

    public abstract AuthenticatedAccountDto Register(RegisterAccountDto registerAccountDto);
    
    public abstract AuthenticatedAccountDto Login(LoginAccountDto loginAccountDto);

    public abstract void Logout(string token);
}