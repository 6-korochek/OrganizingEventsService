using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Contracts.Event;

public class UpdateEventDto
{
    public string? Title { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string? Description { get; set; }
    
    public string? MeetingLink { get; set; }
    
    public uint MaxParticipant { get; set; }
    
    public EventStatus EventStatus { get; set; }
}