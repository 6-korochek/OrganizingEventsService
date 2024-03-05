namespace OrganizingEventsService.Application.Models.Dto.Participant;

public class CreateParticipantDto
{
    public string AccountEmail { get; set; } = null!;

    public Guid RoleId { get; set; }
}