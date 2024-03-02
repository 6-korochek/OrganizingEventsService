using System.ComponentModel.DataAnnotations;

namespace OrganizingEventsService.Application.Contracts.Account;

public class AccountDto
{
    [Required(ErrorMessage = "")]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; } 
    
    public bool IsInvite { get; set; }
}