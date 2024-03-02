using OrganizingEventsService.Application.Models.Dto.Role;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.Dto.Participant;

public class ParticipantDto
{
    public Guid AccountId { get; set; }

    public string? AccountName { get; set; }

    public string? AccountSurName { get; set; }

    public EventParticipantInviteStatus Status { get; set; }

    public RoleDto? Role { get; set; }
}