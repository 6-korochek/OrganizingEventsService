using MediatR;
using OrganizingEventsService.Application.Models.Dto.Event;

namespace OrganizingEventsService.Application.Events.Commands;

public class PartiallyUpdateEventCommand: IRequest<Guid>
{
    public Guid EventId { get; set; }
    public UpdateEventDto EventRequest { get; set; } = null!;
}