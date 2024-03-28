using MediatR;
using OrganizingEventsService.Application.Models.Dto.Participant;

namespace OrganizingEventsService.Application.Events.Queries;

public class GetMemberEventQuery: IRequest<ParticipantDto>
{
    public Guid EventId { get; set; }
    public Guid AccountId { get; set; }
}