using MediatR;
using OrganizingEventsService.Application.Models.Dto.Participant;

namespace OrganizingEventsService.Application.Events.Commands;

public class CreateMembersEventCommand: IRequest
{
    public Guid EventId { get; set; }
    public IEnumerable<CreateParticipantDto> EventRequest { get; set; } = null!;

}