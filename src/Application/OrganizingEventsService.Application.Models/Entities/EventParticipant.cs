using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class EventParticipant
{
    public Guid Pk { get; set; }

    public Guid EventPk { get; set; }

    public Guid AccountPk { get; set; }

    public EventParticipantInviteStatus InviteStatus { get; set; }

    public bool? IsBanned { get; set; }

    public Guid RolePk { get; set; }

    public virtual Account AccountPkNavigation { get; set; } = null!;

    public virtual Event EventPkNavigation { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get;  } = new List<Feedback>();

    public virtual Role RolePkNavigation { get; set; } = null!;
}
