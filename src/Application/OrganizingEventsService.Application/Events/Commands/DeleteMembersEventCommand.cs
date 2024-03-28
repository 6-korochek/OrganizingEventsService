using MediatR;

namespace OrganizingEventsService.Application.Events.Commands;

public class DeleteMembersEventCommand : IRequest
{
    public Guid EventId { get; set; }

    public IEnumerable<string> AccountEmails { get; set; } = null!;
}