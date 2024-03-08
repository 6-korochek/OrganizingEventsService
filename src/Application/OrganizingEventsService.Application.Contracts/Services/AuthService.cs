using Microsoft.Extensions.Options;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Contracts.Services;

public abstract class AuthService
{
    protected IAccountService AccountService { get; }
    protected readonly JwtSettings _jwtsettings;

    protected AuthService(IAccountService accountService, IOptions<JwtSettings> jwtsettings)
    {
        AccountService = accountService;
        _jwtsettings = jwtsettings.Value;
    }

    public abstract AuthenticatedAccountDto AuthenticateAccount(string token);

    public abstract Task<AuthenticatedAccountDto> Register(RegisterAccountDto registerAccountDto);

    public abstract Task<AuthenticatedAccountDto> Login(LoginAccountDto loginAccountDto);

}