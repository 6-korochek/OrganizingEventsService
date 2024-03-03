using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IEventRepository : IBaseRepository<Event>
{
    IAsyncEnumerable<Event> GetListByQuery(EventQuery query);
    
    Task<Event> GetEventByInviteCode(string inviteCode);

    IAsyncEnumerable<EventParticipant> GetParticipantListByEventId(Guid eventId, 
        bool includeRole = false, 
        bool includeAccount = false);

    Task<EventParticipant> GetParticipantById(Guid id);

    Task AddParticipants(Guid eventId, IEnumerable<EventParticipant> eventParticipants);
    
    Task UpdateParticipant(EventParticipant eventParticipant);

    Task DeleteParticipant(EventParticipant eventParticipant);

    IAsyncEnumerable<Feedback> GetFeedbackListByEventId(Guid eventId, bool includeAuthor = false);
}