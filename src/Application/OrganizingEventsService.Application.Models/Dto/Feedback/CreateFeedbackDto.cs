namespace OrganizingEventsService.Application.Models.Dto.Feedback;

public class CreateFeedbackDto
{
    public uint Rating { get; set; }

    public string? Text { get; set; }
}