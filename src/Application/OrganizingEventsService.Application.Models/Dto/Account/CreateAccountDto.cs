namespace OrganizingEventsService.Application.Models.Dto.Account;

public class CreateAccountDto
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }
}