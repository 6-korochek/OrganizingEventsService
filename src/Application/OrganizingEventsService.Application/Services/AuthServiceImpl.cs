using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganizingEventsService.Application.Abstractions.Exceptions;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace OrganizingEventsService.Application.Services;

public class AuthServiceImpl : Contracts.Services.AuthService
{
    public AuthServiceImpl(IAccountService accountService, IOptions<JwtSettings> jwtsettings) : base(accountService,
        jwtsettings) { }

    public override AuthenticatedAccountDto AuthenticateAccount(string token)
    {
        byte[] key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
        var securityKey = new SymmetricSecurityKey(key);

        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            IssuerSigningKey = securityKey
        };

        ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken _);

        string? userId = principal?.FindFirst("user_id")?.Value;
        string? iat = principal?.FindFirst("iat")?.Value;

        if (userId == null)
        {
            throw new UnauthorizedException();
        }

        var account = AccountService.GetAccountById(Guid.Parse(userId));

        if (iat != null && DateTime.Compare(account.PasswordHashUpdatedAt, DateTime.Parse(iat)) > 0)
        {
            throw new Exception(); // Потом кастомные добавим
        }
        return new AuthenticatedAccountDto
        {
            Token = token,
            Account = account
        };
    }

    public override AuthenticatedAccountDto Register(RegisterAccountDto registerAccountDto)
    {
        var createAccountDto = new CreateAccountDto
        {
            Name = registerAccountDto.Name,
            Surname = registerAccountDto.Surname,
            Email = registerAccountDto.Email,
            Password = registerAccountDto.Password
        };

        var account = AccountService.CreateAccount(createAccountDto);

        var accessToken = GenerateAccessToken(account.Id);
        return new AuthenticatedAccountDto
        {
            Token = accessToken,
            Account = account
        };
    }

    public override AuthenticatedAccountDto Login(LoginAccountDto loginAccountDto)
    {

        // по емаил и сравниваю пароли

        var account = AccountService.GetAccountByEmail(loginAccountDto.Email);
        if (!BCrypt.Net.BCrypt.Verify(loginAccountDto.Password,account.PasswordHash))
        {
            throw new Exception(); // Потом кастомные добавим
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
                new Claim("iat", DateTime.UtcNow.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}