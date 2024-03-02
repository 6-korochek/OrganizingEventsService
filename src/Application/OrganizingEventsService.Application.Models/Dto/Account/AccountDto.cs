namespace OrganizingEventsService.Application.Models.Dto.Account;

public class AccountDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; } 
    
    public bool IsInvite { get; set; }
}