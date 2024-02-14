namespace OrganizingEventsService.Infrastructure.Persistence.Entities;

public partial class Account
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? IsInvite { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<EventParticipant> EventParticipants { get;  } = new List<EventParticipant>();
}
