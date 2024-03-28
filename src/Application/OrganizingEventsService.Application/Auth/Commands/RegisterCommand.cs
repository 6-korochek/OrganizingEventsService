using MediatR;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Auth.Commands;

public class RegisterCommand : IRequest<AuthenticatedAccountDto>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}