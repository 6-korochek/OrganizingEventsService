namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IBaseRepository<TEntity>
{
    public abstract TEntity GetById(Guid id);

    public abstract void Add(TEntity entity);

    public abstract void Update(TEntity entity);

    public abstract void DeleteById(Guid id);
}