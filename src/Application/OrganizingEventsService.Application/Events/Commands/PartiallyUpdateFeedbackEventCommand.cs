using MediatR;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;

namespace OrganizingEventsService.Application.Events.Commands;

public class PartiallyUpdateFeedbackEventCommand: IRequest<FeedbackDto>
{
    public Guid FeedbackId { get; set; }
    public UpdateFeedbackDto EventRequest { get; set; } = null!;
}