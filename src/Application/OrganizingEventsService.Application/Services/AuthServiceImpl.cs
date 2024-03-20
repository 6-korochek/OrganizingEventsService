using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganizingEventsService.Application.Services;

public class AuthServiceImpl : AuthService
{
    public AuthServiceImpl(IAccountService accountService, IOptions<JwtSettings> jwtsettings) : base(accountService,
        jwtsettings) { }

    public override async Task<AuthenticatedAccountDto> AuthenticateAccount(string token)
    {
        byte[] key = Encoding.UTF8.GetBytes(_jwtsettings.SecretKey);
        var securityKey = new SymmetricSecurityKey(key);

        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = securityKey
        };

        ClaimsPrincipal principal;
        try
        {
            principal =
                new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken _);
        }
        catch
        {
            throw new UnauthorizedException();
        }

        string? userId = principal?.FindFirst("user_id")?.Value;
        string? iat = principal?.FindFirst("iat")?.Value;

        if (userId == null)
        {
            throw new UnauthorizedException();
        }

        AccountDto account;
        try
        {
            account = await AccountService.GetAccountById(Guid.Parse(userId));
        }
        catch (NotFoundException)
        {
            throw new UnauthorizedException();
        }

        if (iat != null && account.PasswordHashUpdatedAt > DateTimeOffset.FromUnixTimeSeconds(long.Parse(iat)).DateTime)
        {
            throw new UnauthorizedException();
        }

        return new AuthenticatedAccountDto
        {
            Token = token,
            Account = account
        };
    }

    public override async Task<AuthenticatedAccountDto> Register(RegisterAccountDto registerAccountDto)
    {
        var createAccountDto = new CreateAccountDto
        {
            Name = registerAccountDto.Name,
            Surname = registerAccountDto.Surname,
            Email = registerAccountDto.Email,
            Password = registerAccountDto.Password
        };

        var account = await AccountService.CreateAccount(createAccountDto);

        var accessToken = GenerateAccessToken(account.Id);
        return new AuthenticatedAccountDto
        {
            Token = accessToken,
            Account = account
        };
    }

    public override async Task<AuthenticatedAccountDto> Login(LoginAccountDto loginAccountDto)
    {
        // по емаил и сравниваю пароли

        var account = await AccountService.GetAccountByEmail(loginAccountDto.Email);
        if (account is null)
        {
            throw new UnauthorizedException();
        }
        if (!BCrypt.Net.BCrypt.Verify(loginAccountDto.Password, account.PasswordHash))
        {
            throw new UnauthorizedException();
        }

        var accessToken = GenerateAccessToken(account.Id);
        return new AuthenticatedAccountDto
        {
            Token = accessToken,
            Account = account
        };
    }

    private string GenerateAccessToken(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("user_id", Convert.ToString(userId)!),
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}