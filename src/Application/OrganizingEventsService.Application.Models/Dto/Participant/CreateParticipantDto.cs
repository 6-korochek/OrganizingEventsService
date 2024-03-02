namespace OrganizingEventsService.Application.Models.Dto.Participant;

public class CreateParticipantDto
{
    public string? AccountEmail { get; set; }

    public Guid RoleId { get; set; }
}