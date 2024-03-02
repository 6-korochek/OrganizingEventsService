using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IEventRepository : IBaseRepository<Event>
{
    IEnumerable<Event> GetListByQuery(EventQuery query);

    IEnumerable<EventParticipant> GetParticipantListByQuery(EventParticipantQuery query);

    EventParticipant GetParticipantById(Guid id);

    EventParticipant GetEventByInviteCode(string inviteCode);

    void UpdateParticipant(EventParticipant eventParticipant);

    void DeleteParticipant(EventParticipant eventParticipant);
}