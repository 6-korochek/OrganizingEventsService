namespace OrganizingEventsService.Application.Contracts.Feedback;

public class AuthorDto
{
    public Guid AccountPk { get; set; }
    
    public string? Name { get; set; }
    
    public string? SurName { get; set; }
}