using MediatR;

namespace OrganizingEventsService.Application.Events.Commands;

public class DeleteEventCommand: IRequest
{
    public Guid EventId { get; set; }
}