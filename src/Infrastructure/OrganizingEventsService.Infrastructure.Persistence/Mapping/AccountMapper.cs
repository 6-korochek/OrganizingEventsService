using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Mapping;

public static class AccountMapper
{
    public static Account ToEntity(AccountModel accountModel)
    {
        return new Account
        {
            Id = accountModel.Id,
            Name = accountModel.Name,
            Surname = accountModel.Surname,
            Email = accountModel.Email,
            PasswordHash = accountModel.PasswordHash,
            IsInvite = accountModel.IsInvite,
            IsAdmin = accountModel.IsAdmin,
            CreatedAt = accountModel.CreatedAt,
            PasswordHashUpdatedAt = accountModel.PasswordHashUpdatedAt
        };
    }

    public static AccountModel ToModel(Account account)
    {
        return new AccountModel
        {
            Id = account.Id,
            Name = account.Name,
            Surname = account.Surname,
            Email = account.Email,
            PasswordHash = account.PasswordHash,
            IsInvite = account.IsInvite,
            IsAdmin = account.IsAdmin,
            CreatedAt = account.CreatedAt,
            PasswordHashUpdatedAt = account.PasswordHashUpdatedAt
        };
    }
}