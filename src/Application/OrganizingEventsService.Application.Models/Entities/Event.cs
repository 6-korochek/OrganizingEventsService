#pragma warning disable CA1716

using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal MaxParticipant { get; set; }

    public string? MeetingLink { get; set; }

    public DateTime? StartDatetime { get; set; }

    public DateTime? EndDatetime { get; set; }

    public EventStatus Status { get; set; }

    public string? InviteCode { get; set; }

    public virtual ICollection<EventParticipant> EventParticipants { get;  } = new List<EventParticipant>();
}
