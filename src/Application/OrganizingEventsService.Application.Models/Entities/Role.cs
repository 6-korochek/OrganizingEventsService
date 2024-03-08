#pragma warning disable CA1724

namespace OrganizingEventsService.Application.Models.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventParticipant> EventParticipants { get; } = new List<EventParticipant>();
}
