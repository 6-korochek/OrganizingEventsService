using MediatR;

namespace OrganizingEventsService.Application.Events.Commands;

public class DeleteAccountCommand: IRequest
{
    public Guid AccountId { get; set; }
}