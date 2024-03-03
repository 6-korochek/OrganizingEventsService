using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Mapping;
using OrganizingEventsService.Infrastructure.Persistence.Models;
using EventStatus = OrganizingEventsService.Application.Models.Entities.Enums.EventStatus;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public class EventRepository : BaseRepository<Event, EventModel>, IEventRepository
{
    public EventRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Events) { }
    
    public IAsyncEnumerable<Event> GetListByQuery(EventQuery query)
    {
        IQueryable<EventModel> queryable = DbContext.Events;
        if (query.Ids.Any())
        {
            queryable = queryable.Where(model => query.Ids.Contains(model.Id));
        }

        if (query.Statuses.Any())
        {
            queryable = queryable.Where(model => query.Statuses.Contains((EventStatus)model.Status));
        }

        if (query.IncludeParticipants)
        {
            queryable = queryable.Include(model => model.EventParticipants);
        }

        if (query.Offset is not null)
        {
            queryable = queryable.Skip((int)query.Offset);
        }

        if (query.Limit is not null)
        {
            queryable = queryable.Take((int)query.Limit);
        }

        return queryable.AsAsyncEnumerable().Select(EventMapper.ToEntity);
    }
    
    public async Task<Event> GetEventByInviteCode(string inviteCode)
    {
        EventModel? model = await DbContext.Events.FirstOrDefaultAsync(model => model.InviteCode == inviteCode);
        if (model is null)
        {
            throw new Exception(); // Потом кастомные добавим 
        }

        return MapToEntity(model);
    }
    
    public IAsyncEnumerable<EventParticipant> GetParticipantListByEventId(
        Guid eventId, 
        bool includeRole = false, 
        bool includeAccount = false)
    {
        IQueryable<EventParticipantModel> queryable = DbContext.EventParticipants.Where(model => model.EventId == eventId);
        if (includeRole)
        {
            queryable = queryable.Include(model => model.RoleIdNavigation);
        }

        if (includeAccount)
        {
            queryable = queryable.Include(model => model.AccountIdNavigation);
        }

        return queryable.AsAsyncEnumerable().Select(EventParticipantMapper.ToEntity);
    }

    public async Task<EventParticipant> GetParticipantById(Guid id)
    {
        EventParticipantModel? model = await DbContext.EventParticipants
            .Include(model => model.RoleIdNavigation)
            .Include(model => model.AccountIdNavigation)
            .FirstOrDefaultAsync(model => model.Id == id);
        
        if (model is null)
        {
            throw new Exception(); // Потом кастомные добавим 
        }

        return EventParticipantMapper.ToEntity(model);
    }

    public async Task AddParticipants(Guid eventId, IEnumerable<EventParticipant> eventParticipants)
    {
        var existsParticipant = DbContext.EventParticipants.Where(model => model.EventId == eventId);
        var newParticipants =
            eventParticipants
                .Select(EventParticipantMapper.ToModel)
                .ExceptBy(existsParticipant.Select(model => model.Id), model => model.Id);
        
        await DbContext.EventParticipants.AddRangeAsync(newParticipants);
        await DbContext.SaveChangesAsync();
    }

    public async Task UpdateParticipant(EventParticipant eventParticipant)
    {
        EventParticipantModel newModelState = EventParticipantMapper.ToModel(eventParticipant);
        EventParticipantModel? currentModelState = await DbContext.EventParticipants.FindAsync(newModelState.Id);
        DbContext.Entry(currentModelState!).CurrentValues.SetValues(newModelState);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteParticipant(EventParticipant eventParticipant)
    {
        await DbContext.EventParticipants.Where(model => model.Id == eventParticipant.Id).ExecuteDeleteAsync();
    }

    public IAsyncEnumerable<Feedback> GetFeedbackListByEventId(Guid eventId, bool includeAuthor = false)
    {
        IQueryable<FeedbackModel> queryable =
            DbContext.Feedbacks.Where(model => model.EventParticipantIdNavigation.EventId == eventId);

        if (includeAuthor)
        {
            queryable
                .Include(model => model.EventParticipantIdNavigation)
                .Include(model => model.EventParticipantIdNavigation.AccountIdNavigation);
        }

        return queryable.AsAsyncEnumerable().Select(FeedbackMapper.ToEntity);
    }

    protected override Event MapToEntity(EventModel model)
    {
        return EventMapper.ToEntity(model);
    }

    protected override EventModel MapToModel(Event entity)
    {
        return EventMapper.ToModel(entity);
    }
}