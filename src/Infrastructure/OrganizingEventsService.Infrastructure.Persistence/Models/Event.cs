using OrganizingEventsService.Infrastructure.Persistence.Models.Enums;

namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class EventModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal MaxParticipant { get; set; }

    public string? MeetingLink { get; set; }

    public DateTime? StartDatetime { get; set; }

    public DateTime? EndDatetime { get; set; }

    public EventParticipantInviteStatus Status { get; set; }

    public string? InviteCode { get; set; }

    public virtual ICollection<EventParticipantModel> EventParticipants { get;  } = new List<EventParticipantModel>();
}
