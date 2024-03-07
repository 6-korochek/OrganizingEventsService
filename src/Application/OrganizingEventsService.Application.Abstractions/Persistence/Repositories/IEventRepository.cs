using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IEventRepository : IBaseRepository<Event>
{
    IAsyncEnumerable<Event> GetListByQuery(EventQuery query);

    IAsyncEnumerable<EventParticipant> GetParticipantListByQuery(EventParticipantQuery query);
    
    Task<Event> GetEventByInviteCode(string inviteCode);

    Task<EventParticipant> GetParticipantInEvent(
        Guid accountId,
        Guid eventId,
        bool includeRole = false,
        bool includeAccount = false);

    Task AddParticipants(Guid eventId, IEnumerable<EventParticipant> eventParticipants);
    
    Task UpdateParticipant(EventParticipant eventParticipant);

    Task DeleteParticipant(EventParticipant eventParticipant);
}