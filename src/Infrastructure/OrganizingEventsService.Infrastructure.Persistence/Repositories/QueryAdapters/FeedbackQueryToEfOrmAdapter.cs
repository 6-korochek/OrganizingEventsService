using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

public class FeedbackQueryToEfOrmAdapter(FeedbackQuery query, ApplicationDbContext dbContext) 
    : QueryToEfOrmAdapter<FeedbackQuery, FeedbackModel>(query, dbContext)
{
    public override IQueryable<FeedbackModel> Adapt()
    {
        IQueryable<FeedbackModel> queryable = DbContext.Feedbacks;
        if (Query.EventIds.Any())
        {
            queryable = queryable.Where(model => Query.EventIds.Contains(model.EventParticipantIdNavigation.EventId));
        }
        
        if (Query.IncludeAuthor)
        {
            queryable
                .Include(model => model.EventParticipantIdNavigation)
                .ThenInclude(model => model.AccountIdNavigation);
        }

        return queryable;
    }
}