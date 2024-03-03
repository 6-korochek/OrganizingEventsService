using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Models.Enums;

namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class EventModel : IEntity
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

    public virtual ICollection<EventParticipantModel> EventParticipants { get; set; } = new List<EventParticipantModel>();
}
