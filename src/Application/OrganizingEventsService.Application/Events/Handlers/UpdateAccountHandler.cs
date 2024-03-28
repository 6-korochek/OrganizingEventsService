using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;

namespace OrganizingEventsService.Application.Events.Handlers;

public class UpdateAccountHandler: IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;

    public UpdateAccountHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.GetById(request.AccountId);

        accountEntity.Name = request.UpdateAccountDto.Name ?? accountEntity.Name;
        accountEntity.Surname = request.UpdateAccountDto.Surname ?? accountEntity.Surname;
        accountEntity.IsInvite = request.UpdateAccountDto.IsInvite ?? accountEntity.IsInvite;
        await _accountRepository.Update(accountEntity);

        return accountEntity.Id;
    }
}