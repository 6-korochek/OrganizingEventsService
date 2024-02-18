namespace OrganizingEventsService.Application.Contracts.Event;

public class NewEventDto
{
    public Guid Id { get; set; }
    
    public string? InviteCode {get; set; }
}