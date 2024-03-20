using MediatR;
using OrganizingEventsService.Application.Models.Dto.Event;

namespace OrganizingEventsService.Application.CQRS.Commands.CreateEvent;

public class CreateEventCommand : IRequest<NewEventDto>
{
    public Guid OrganizerId { get; set; }
    public CreateEventDto EventRequest { get; set; } = null!;
}