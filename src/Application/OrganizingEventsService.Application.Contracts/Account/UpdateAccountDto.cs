namespace OrganizingEventsService.Application.Contracts.Account;

public class UpdateAccountDto
{
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    public bool? IsInvite { get; set; }
}