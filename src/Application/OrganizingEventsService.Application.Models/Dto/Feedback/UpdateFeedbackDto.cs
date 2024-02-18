namespace OrganizingEventsService.Application.Contracts.Feedback;

public class UpdateFeedbackDto
{
    public uint Rating { get; set; }
    
    public string? Text { get; set; }
}