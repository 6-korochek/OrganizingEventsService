using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IFeedbackRepository : IBaseRepository<Feedback>
{
    IAsyncEnumerable<Feedback> GetListByQuery(FeedbackQuery query);
}