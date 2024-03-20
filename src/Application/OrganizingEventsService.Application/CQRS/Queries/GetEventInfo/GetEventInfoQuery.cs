using MediatR;
using OrganizingEventsService.Application.Models.Dto.Event;

namespace OrganizingEventsService.Application.CQRS.Queries.GetEventInfo;

public class GetEventInfoQuery : IRequest<EventDto>
{
    public Guid? EventId { get; set; } = null;
    public string? InviteCode { get; set; } = null;
}