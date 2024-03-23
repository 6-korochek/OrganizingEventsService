using MediatR;
using OrganizingEventsService.Application.Models.Dto.Participant;

namespace OrganizingEventsService.Application.Events.Commands;

public class PartiallyUpdateMemberEventCommand:IRequest<ParticipantDto>
{
    public Guid EventId { get; set; }
    public Guid AccountId { get; set; }
    public UpdateParticipantDto EventRequest { get; set; } = null!;
}