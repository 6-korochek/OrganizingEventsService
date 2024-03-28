using MediatR;
using OrganizingEventsService.Application.Models.Dto.Event;

namespace OrganizingEventsService.Application.Events.Queries;

public class GetEventInfoEventQuery : IRequest<EventDto>
{
    public Guid? EventId { get; set; }
    public string? InviteCode { get; set; }
}