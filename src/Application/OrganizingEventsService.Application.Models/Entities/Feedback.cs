namespace OrganizingEventsService.Application.Models.Entities;

public partial class Feedback
{
    public Guid Id { get; set; }

    public Guid EventParticipantId { get; set; }

    public decimal Rating { get; set; }

    public string? Text { get; set; }

    public virtual EventParticipant EventParticipantIdNavigation { get; set; } = null!;
}
