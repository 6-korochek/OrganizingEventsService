using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.Dto.Participant;

public class UpdateParticipantDto
{
    public EventParticipantInviteStatus? InviteStatus { get; set; }

    public bool? IsBanned { get; set; }

    public Guid? RoleId { get; set; }
}