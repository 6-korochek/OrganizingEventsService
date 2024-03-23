using MediatR;
using OrganizingEventsService.Application.Models.Dto.Feedback;

namespace OrganizingEventsService.Application.Events.Queries;

public class GetFeedbacksEventQuery: IRequest<IEnumerable<FeedbackDto>>

{
    public Guid EventId { get; set; } = Guid.Empty;
}