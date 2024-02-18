namespace OrganizingEventsService.Application.Contracts.Event;

public class OrganizerDto
{
    public Guid AccountId { get; set; }
    
    public string? Name { get; set; }
    
    public string? SurName { get; set; }
}