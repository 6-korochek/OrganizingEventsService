using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Infrastructure.Persistence.Models;

public partial class RoleModel : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventParticipantModel> EventParticipants { get;  } = new List<EventParticipantModel>();

}
