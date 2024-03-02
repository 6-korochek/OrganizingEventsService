namespace OrganizingEventsService.Application.Models.Dto.Event;

public class NewEventDto
{
    public Guid Id { get; set; }

    public string? InviteCode { get; set; }
}