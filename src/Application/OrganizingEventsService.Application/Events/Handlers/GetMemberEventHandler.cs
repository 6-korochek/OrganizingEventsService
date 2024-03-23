using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Dto.Role;

namespace OrganizingEventsService.Application.Events.Handlers;

public class GetMemberEventHandler : IRequestHandler<GetMemberEventQuery, ParticipantDto>
{
    private readonly IEventRepository _eventRepository;

    public GetMemberEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<ParticipantDto> Handle(GetMemberEventQuery request, CancellationToken cancellationToken)
    {
        var participantEntity = await _eventRepository.GetParticipantInEvent(request.AccountId, request.EventId, true, true);
        if (participantEntity is null)
        {
            return null!;
        }
        var participantDto = new ParticipantDto()
        {
            AccountId = participantEntity.AccountId,
            AccountName = participantEntity!.AccountIdNavigation!.Name,
            AccountSurName = participantEntity.AccountIdNavigation.Surname,
            Role = new RoleDto()
            {
                Id = participantEntity.RoleId,
                Name = participantEntity!.RoleIdNavigation!.Name
            }
        };

        return participantDto;
    }
}