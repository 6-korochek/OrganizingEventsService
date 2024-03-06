using OrganizingEventsService.Infrastructure.Persistence.Contexts;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories.QueryAdapters;

public abstract class QueryToEfOrmAdapter<TQuery, TEfModel> where TEfModel : class
{
    protected TQuery Query { get; }
    
    protected ApplicationDbContext DbContext { get; }
    
    protected QueryToEfOrmAdapter(TQuery query, ApplicationDbContext dbContext)
    {
        Query = query;
        DbContext = dbContext;
    }
    
    public abstract IQueryable<TEfModel> Adapt();
}