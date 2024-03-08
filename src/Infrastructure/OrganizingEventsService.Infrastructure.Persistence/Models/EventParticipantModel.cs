using OrganizingEventsService.Infrastructure.Persistence.Models.Enums;

namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class EventParticipantModel
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Guid AccountId { get; set; }

    public EventParticipantInviteStatus InviteStatus { get; set; }

    public bool IsBanned { get; set; }

    public bool? IsArchive { get; set; }

    public Guid RoleId { get; set; }

    public virtual AccountModel? AccountIdNavigation { get; set; }

    public virtual EventModel? EventIdNavigation { get; set; }

    public virtual ICollection<FeedbackModel> Feedbacks { get; } = new List<FeedbackModel>();

    public virtual RoleModel? RoleIdNavigation { get; set; }
}