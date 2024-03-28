using MediatR;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;

namespace OrganizingEventsService.Application.Events.Commands;

public class CreateFeedbackEventCommand: IRequest<Guid>
{
    public Guid EventId { get; set; }
    public CreateFeedbackDto EventRequest { get; set; } = null!;
}