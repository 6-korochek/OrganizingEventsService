namespace OrganizingEventsService.Application.Models.Dto.Feedback;

public class UpdateFeedbackDto
{
    public decimal? Rating { get; set; }

    public string? Text { get; set; }
}