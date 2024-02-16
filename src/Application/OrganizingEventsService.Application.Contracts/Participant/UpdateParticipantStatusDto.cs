using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Contracts.Participant;

public class UpdateParticipantStatusDto
{
    public Guid? EventPk { get; set; }
    
    public EventParticipantInviteStatus Status { get; set; }
}