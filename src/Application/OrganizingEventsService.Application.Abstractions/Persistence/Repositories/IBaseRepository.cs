namespace OrganizingEventsService.Application.Abstractions.Persistence.Repositories;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> GetById(Guid id);

    Task Add(TEntity entity);

    Task Update(TEntity entity);

    Task DeleteById(Guid id);
}