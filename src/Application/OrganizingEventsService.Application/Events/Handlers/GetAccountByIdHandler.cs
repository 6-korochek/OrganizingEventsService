using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.CQRS.Queries;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Events.Handlers;

public class GetAccountByIdHandler: IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.GetById(request.AccountId);
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
}