namespace OrganizingEventsService.Application.Contracts.Account;

public class AccountDto
{
    public Guid Pk { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; } 
    
    public bool IsInvite { get; set; }
}