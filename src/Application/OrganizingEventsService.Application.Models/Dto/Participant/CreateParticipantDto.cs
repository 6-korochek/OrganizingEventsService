namespace OrganizingEventsService.Application.Contracts.Participant;

public class CreateParticipantDto
{
    public string? AccountEmail { get; set; }

    private Guid RoleId { get; set; }
}