namespace OrganizingEventsService.Application.Contracts.Feedback;

public class AuthorDto
{
    public Guid AccountId { get; set; }
    
    public string? Name { get; set; }
    
    public string? SurName { get; set; }
}