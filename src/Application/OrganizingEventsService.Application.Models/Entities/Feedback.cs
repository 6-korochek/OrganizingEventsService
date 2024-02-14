namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class Feedback
{
    public Guid Id { get; set; }

    public Guid EventParticipantPk { get; set; }

    public decimal Rating { get; set; }

    public string? Text { get; set; }

    public virtual EventParticipant EventParticipantPkNavigation { get; set; } = null!;
}
