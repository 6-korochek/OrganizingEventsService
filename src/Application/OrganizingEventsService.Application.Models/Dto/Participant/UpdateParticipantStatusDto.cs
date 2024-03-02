using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.Dto.Participant;

public class UpdateParticipantStatusDto
{
    public Guid? EventId { get; set; }

    public EventParticipantInviteStatus Status { get; set; }
}