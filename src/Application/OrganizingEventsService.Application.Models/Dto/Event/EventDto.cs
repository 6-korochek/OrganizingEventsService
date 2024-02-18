using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Contracts.Event;

public class EventDto
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

    public OrganizerDto? Organizer { get; set; }
}