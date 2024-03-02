namespace OrganizingEventsService.Application.Models.Dto.Feedback;

public class FeedbackDto
{
    public Guid Id { get; set; }

    public uint Rating { get; set; }

    public string? Text { get; set; }

    public AuthorDto? Author { get; set; }
}