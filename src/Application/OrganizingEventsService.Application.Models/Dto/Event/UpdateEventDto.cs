using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.Dto.Event;

public class UpdateEventDto
{
    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }

    public string? MeetingLink { get; set; }

    public uint? MaxParticipant { get; set; }

    public EventStatus? EventStatus { get; set; }
}