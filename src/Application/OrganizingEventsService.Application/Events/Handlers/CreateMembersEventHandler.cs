using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class CreateMembersEventHandler : IRequestHandler<CreateMembersEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IAccountRepository _accountRepository;

    public CreateMembersEventHandler(IEventRepository eventRepository,  IAccountRepository accountRepository)
    {
        _eventRepository = eventRepository;
        _accountRepository = accountRepository;
    }


    public async Task Handle(CreateMembersEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetById(request.EventId);

        IEnumerable<CreateParticipantDto> createParticipantDtos = request.EventRequest as CreateParticipantDto[] ?? request.EventRequest.ToArray();
        var accountEmails = from dto in createParticipantDtos select dto.AccountEmail;



        var accountQuery = new AccountQuery().WithEmail(accountEmails);
        var accounts = await _accountRepository.GetListByQuery(accountQuery).ToListAsync();
        var existingAccountDtoList = accounts.Select(account => new AccountDto
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

        // IEnumerable<AccountDto> accountDtoList = existingAccountDtoList as AccountDto[] ?? existingAccountDtoList.ToArray();
        if (!existingAccountDtoList.Any())
        {
            return;
        }

        var newEventParticipants = from createParticipantDto in createParticipantDtos
            where existingAccountDtoList.Select(acc => acc.Email)
                .Contains(createParticipantDto.AccountEmail)
            let account = existingAccountDtoList.First(c =>
                c.Email == createParticipantDto.AccountEmail)
            select new EventParticipant()
            {
                Id = Guid.NewGuid(),
                EventId = request.EventId,
                AccountId = account.Id,
                InviteStatus = EventParticipantInviteStatus.Pending,
                RoleId = createParticipantDto.RoleId
            };

        await _eventRepository.AddParticipants(eventEntity.Id, newEventParticipants);
    }
}