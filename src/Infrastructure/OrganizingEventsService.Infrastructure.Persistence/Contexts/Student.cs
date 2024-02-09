namespace OrganizingEventsService.Infrastructure.Persistence.Contexts;

public class Student
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public int Age { get; set; }
}