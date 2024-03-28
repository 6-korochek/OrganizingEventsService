using MediatR;
using OrganizingEventsService.Application.Models.Dto.Participant;

namespace OrganizingEventsService.Application.Events.Queries;

public class GetMembersEventQuery : IRequest<IAsyncEnumerable<ParticipantDto>>
{
    public Guid EventId { get; set; } = Guid.Empty;
}