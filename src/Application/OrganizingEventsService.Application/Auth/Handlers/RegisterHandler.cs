using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Auth.Commands;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganizingEventsService.Application.Auth.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, AuthenticatedAccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly JwtSettings _jwtsettings;

    public RegisterHandler(IAccountRepository accountRepository, IOptions<JwtSettings> jwtsettings)
    {
        _accountRepository = accountRepository;
        _jwtsettings = jwtsettings.Value;
    }

    public async Task<AuthenticatedAccountDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var accountQuery = new AccountQuery().WithEmail(request.Email);
        var accountList = _accountRepository.GetListByQuery(accountQuery);
        if (await accountList.CountAsync() != 0)
        {
            throw new ConflictException(message: "An account with that e-mail already exists.");
        }
        
        var accountEntity = new Account
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };
        
        await _accountRepository.Add(accountEntity);
        var account = new AccountDto
        {
            Id = accountEntity.Id,
            Email = accountEntity.Email,
            Name = accountEntity.Name,
            Surname = accountEntity.Surname,
            IsInvite = accountEntity.IsInvite,
            PasswordHash = accountEntity.PasswordHash,
            PasswordHashUpdatedAt = accountEntity.PasswordHashUpdatedAt,
            IsAdmin = accountEntity.IsAdmin
        };
        
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