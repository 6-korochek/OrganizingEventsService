using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Mapping;
using OrganizingEventsService.Infrastructure.Persistence.Models;
using OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public class FeedbackRepository : BaseRepository<Feedback, FeedbackModel>, IFeedbackRepository
{
    public FeedbackRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Feedbacks) { }

    public IAsyncEnumerable<Feedback> GetListByQuery(FeedbackQuery query)
    {
        var queryAdapter = new FeedbackQueryToEfOrmAdapter(query, DbContext);
        return queryAdapter.Adapt().AsAsyncEnumerable().Select(FeedbackMapper.ToEntity);
    }

    protected override Feedback MapToEntity(FeedbackModel model)
    {
        return FeedbackMapper.ToEntity(model);
    }

    protected override FeedbackModel MapToModel(Feedback entity)
    {
        return FeedbackMapper.ToModel(entity);
    }
}