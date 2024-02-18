using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Contracts.Participant;

public class UpdateParticipantDto
{
    public EventParticipantInviteStatus InviteStatus { get; set; }

    public bool? IsBanned { get; set; }

    public Guid RoleId { get; set; }
}