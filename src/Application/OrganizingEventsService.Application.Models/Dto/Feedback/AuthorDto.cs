namespace OrganizingEventsService.Application.Models.Dto.Feedback;

public class AuthorDto
{
    public Guid AccountId { get; set; }

    public string? Name { get; set; }

    public string? SurName { get; set; }
}