using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class FeedbackModel : IEntity
{
    public Guid Id { get; set; }

    public Guid EventParticipantId { get; set; }

    public decimal Rating { get; set; }

    public string? Text { get; set; }

    public virtual EventParticipantModel EventParticipantIdNavigation { get; set; } = null!;
}
