using MediatR;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Events.Commands;

public class UpdateAccountCommand: IRequest<Guid>
{
    public Guid AccountId { get; set; }
    public UpdateAccountDto UpdateAccountDto { get; set; } = null!;
}