using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<TEntity, TModel>
    (ApplicationDbContext dbContext, DbSet<TModel> dbSet) : IBaseRepository<TEntity>
    where TModel : class, IEntity
{
    protected ApplicationDbContext DbContext { get; } = dbContext;

    private DbSet<TModel> DbSet { get; } = dbSet;

    public async Task<TEntity> GetById(Guid id)
    {
        TModel? model = await DbSet.FirstOrDefaultAsync(model => model.Id == id);
        if (model == null)
        {
            throw new Exception(); // Потом кастомные добавим
        }

        return MapToEntity(model);
    }

    public async Task Add(TEntity entity)
    {
        TModel? model = MapToModel(entity);
        await DbSet.AddAsync(model);
        await DbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        TModel newModelState = MapToModel(entity);
        TModel? currentModelState = await DbSet.FindAsync(newModelState.Id);
        DbContext.Entry(currentModelState!).CurrentValues.SetValues(newModelState);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteById(Guid id)
    {
        await DbSet.Where(model => model.Id == id).ExecuteDeleteAsync();
    }

    protected abstract TEntity MapToEntity(TModel model);

    protected abstract TModel MapToModel(TEntity entity);
}