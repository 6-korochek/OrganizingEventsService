using MediatR;

namespace OrganizingEventsService.Application.Events.Commands;

public class DeleteMemberEventCommand: IRequest
{
    public Guid EventId { get; set; }
    public Guid AccountId { get; set; }
}