using MediatR;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Auth.Commands;

public class LoginCommand : IRequest<AuthenticatedAccountDto>
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}