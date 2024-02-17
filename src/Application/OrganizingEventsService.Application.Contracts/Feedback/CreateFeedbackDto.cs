namespace OrganizingEventsService.Application.Contracts.Feedback;

public class CreateFeedbackDto
{
    public uint Rating { get; set; }
    
    public string? Text { get; set; }
}