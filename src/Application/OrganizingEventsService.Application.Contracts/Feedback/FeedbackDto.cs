namespace OrganizingEventsService.Application.Contracts.Feedback;

public class FeedbackDto
{
    public Guid Pk { get; set; }
    
    public uint Rating { get; set; }
    
    public string? Text { get; set; }

    public AuthorDto? Author { get; set; }
}