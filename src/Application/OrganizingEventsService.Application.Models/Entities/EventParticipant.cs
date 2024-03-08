using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.Entities;

public partial class EventParticipant
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Guid AccountId { get; set; }

    public EventParticipantInviteStatus InviteStatus { get; set; }

    public bool IsBanned { get; set; }

    public bool? IsArchive { get; set; }

    public Guid RoleId { get; set; }

    public virtual Account? AccountIdNavigation { get; set; }

    public virtual Event? EventIdNavigation { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    public virtual Role? RoleIdNavigation { get; set; }
}
