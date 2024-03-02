namespace OrganizingEventsService.Application.Models.Dto.Account;

public class UpdateAccountDto
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public bool? IsInvite { get; set; }
}