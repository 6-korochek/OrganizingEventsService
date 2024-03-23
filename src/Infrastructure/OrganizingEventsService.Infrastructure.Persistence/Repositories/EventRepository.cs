using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Mapping;
using OrganizingEventsService.Infrastructure.Persistence.Models;
using OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public class EventRepository : BaseRepository<Event, EventModel>, IEventRepository
{
    public EventRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Events) {}

    public IAsyncEnumerable<Event> GetListByQuery(EventQuery query)
    {
        var queryAdapter = new EventQueryToEfOrmAdapter(query, DbContext);
        return queryAdapter.Adapt().AsAsyncEnumerable().Select(EventMapper.ToEntity);
    }

    public IAsyncEnumerable<EventParticipant> GetParticipantListByQuery(EventParticipantQuery query)
    {
        var queryAdapter = new EventParticipantToEfOrmAdapter(query, DbContext);
        return queryAdapter.Adapt()
            .AsAsyncEnumerable()
            .Select(EventParticipantMapper.ToEntity);
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

    public async Task<EventParticipant> GetParticipantInEvent(
        Guid accountId, 
        Guid eventId,
        bool includeRole = false,
        bool includeAccount = false)
    {
        IQueryable<EventParticipantModel> queryable = DbContext.EventParticipants;
        if (includeRole)
        {
            queryable = queryable.Include(model => model.RoleIdNavigation);
        }

        if (includeAccount)
        {
            queryable = queryable.Include(model => model.AccountIdNavigation);
        }

        EventParticipantModel? model =
            await queryable.FirstOrDefaultAsync(model => model.AccountId == accountId && model.EventId == eventId);

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
                .ExceptBy(existsParticipant.Select(model => model.AccountId), model => model.AccountId);

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

    public Task DeleteParticipant(IEnumerable<EventParticipant> eventParticipant)
    {
        throw new NotImplementedException();
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