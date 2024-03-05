namespace OrganizingEventsService.Application.Models.Dto.Account;

public class AuthenticatedAccountDto
{
    public AccountDto Account { get; set; } = null!;
    
    public string? Token { get; set; }
}