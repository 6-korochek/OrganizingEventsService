using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.Entities;

public partial class EventParticipant
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Guid AccountId { get; set; }

    public EventParticipantInviteStatus InviteStatus { get; set; }

    public bool? IsBanned { get; set; }

    public Guid RoleId { get; set; }

    public virtual Account AccountIdNavigation { get; set; } = null!;

    public virtual Event EventIdNavigation { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get;  } = new List<Feedback>();

    public virtual Role RoleIdNavigation { get; set; } = null!;
}
