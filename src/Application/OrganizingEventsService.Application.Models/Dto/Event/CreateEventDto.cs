#pragma warning disable CA1716

using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Text.Json.Serialization;

namespace OrganizingEventsService.Application.Models.Dto.Event;

public class CreateEventDto
{
    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? MeetingLink { get; set; }

    public uint MaxParticipant { get; set; }

    public EventStatus EventStatus { get; set; }
}