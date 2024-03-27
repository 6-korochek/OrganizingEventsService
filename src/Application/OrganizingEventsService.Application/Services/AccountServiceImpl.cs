using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Services;

public class AccountServiceImpl : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountServiceImpl(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> GetAccountById(Guid accountId)
    {
        var accountEntity = await _accountRepository.GetById(accountId);
        return new AccountDto
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
    }

    public async Task<AccountDto> GetAccountByEmail(string email)
    {
        var accountQuery = new AccountQuery().WithEmail(email);
        var accountList = _accountRepository.GetListByQuery(accountQuery);
        if (await accountList.CountAsync() == 0)
        {
            return null!;
        }

        var accountEntity = await accountList.FirstAsync();
        return new AccountDto
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
    }

    public async Task<IEnumerable<AccountDto>> GetExistingAccountsByEmail(IEnumerable<string> emails)
    {
        var accountQuery = new AccountQuery().WithEmail(emails);
        var accounts = await _accountRepository.GetListByQuery(accountQuery).ToListAsync();
        var accountDtoList = accounts.Select(account => new AccountDto
            {
                Id = account.Id,
                Email = account.Email,
                Name = account.Name,
                Surname = account.Surname,
                IsInvite = account.IsInvite,
                PasswordHash = account.PasswordHash,
                PasswordHashUpdatedAt = account.PasswordHashUpdatedAt
            })
            .ToList();
        return accountDtoList;
    }

    public async Task<AccountDto> UpdateAccount(Guid accountId, UpdateAccountDto updateAccountDto)
    {
        var accountEntity = await _accountRepository.GetById(accountId);

        accountEntity.Name = updateAccountDto.Name ?? accountEntity.Name;
        accountEntity.Surname = updateAccountDto.Surname ?? accountEntity.Surname;
        accountEntity.IsInvite = updateAccountDto.IsInvite ?? accountEntity.IsInvite;
        await _accountRepository.Update(accountEntity);

        return new AccountDto
        {
            Id = accountEntity.Id,
            Email = accountEntity.Email,
            Name = accountEntity.Name,
            Surname = accountEntity.Surname,
            IsInvite = accountEntity.IsInvite,
            PasswordHash = accountEntity.PasswordHash,
            PasswordHashUpdatedAt = accountEntity.PasswordHashUpdatedAt
        };

    }

    public async void DeleteAccountById(Guid accountId)
    {
      await _accountRepository.DeleteById(accountId);
    }

    public async Task<AccountDto> CreateAccount(CreateAccountDto createAccountDto)
    {
        var existingAccount = await GetAccountByEmail(createAccountDto.Email);
        if (existingAccount is not null)
        {
            throw new ConflictException(message: "An account with that e-mail already exists.");
        }
        
        var accountEntity = new Account
        {
            Id = Guid.NewGuid(),
            Email = createAccountDto.Email,
            Name = createAccountDto.Name,
            Surname = createAccountDto.Surname,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createAccountDto.Password),
        };

        await _accountRepository.Add(accountEntity);
        return new AccountDto
        {
            Id = accountEntity.Id,
            Email = accountEntity.Email,
            Name = accountEntity.Name,
            Surname = accountEntity.Surname,
            IsInvite = accountEntity.IsInvite,
            PasswordHash = accountEntity.PasswordHash,
            PasswordHashUpdatedAt = accountEntity.PasswordHashUpdatedAt
        };
    }
}