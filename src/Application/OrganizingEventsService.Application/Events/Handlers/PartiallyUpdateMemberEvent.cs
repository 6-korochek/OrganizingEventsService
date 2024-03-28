using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Dto.Role;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class PartiallyUpdateMemberEventHandler : IRequestHandler<PartiallyUpdateMemberEventCommand, ParticipantDto>
{
    private readonly IEventRepository _eventRepository;

    public PartiallyUpdateMemberEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<ParticipantDto> Handle(PartiallyUpdateMemberEventCommand request, CancellationToken cancellationToken)
    {
        var participantEntity = await _eventRepository.GetParticipantInEvent(request.AccountId, request.EventId, true, true);

        if (participantEntity.IsBanned)
        {
            throw new ConflictException("Cannot update banned user.");
        }

        if (participantEntity.InviteStatus == EventParticipantInviteStatus.Pending)
        {
            participantEntity.InviteStatus = request.EventRequest.InviteStatus ?? participantEntity.InviteStatus;
        }

        participantEntity.IsBanned = request.EventRequest.IsBanned ?? participantEntity.IsBanned;
        participantEntity.RoleId = request.EventRequest.RoleId ?? participantEntity.RoleId;

        await _eventRepository.UpdateParticipant(participantEntity);

        return new ParticipantDto()
        {
            AccountId = participantEntity.AccountId,
            AccountName = participantEntity!.AccountIdNavigation!.Name,
            AccountSurName = participantEntity.AccountIdNavigation.Surname,
            Role = new RoleDto() { Id = participantEntity.RoleId, Name = participantEntity!.RoleIdNavigation!.Name },
            Status = participantEntity.InviteStatus
        };
    }
}