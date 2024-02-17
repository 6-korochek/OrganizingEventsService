namespace OrganizingEventsService.Application.Contracts.Event;

public class NewEventDto
{
    public Guid Pk { get; set; }
    
    public string? InviteCode {get; set; }
}