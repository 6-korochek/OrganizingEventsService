using MediatR;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Queries;

public class GetMyEventQuery : IRequest<IAsyncEnumerable<EventDto>>
{
    public EventStatus Status { get; set; }
    public Guid AccountId { get; set; }
    public PaginationDto? EventRequest { get; set; } = null!;
}