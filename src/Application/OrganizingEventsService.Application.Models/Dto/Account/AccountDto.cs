namespace OrganizingEventsService.Application.Models.Dto.Account;

public class AccountDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsInvite { get; set; }
}