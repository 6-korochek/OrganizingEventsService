using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Contexts;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Repositories;

public class RoleRepository : BaseRepository<Role, RoleModel>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Roles) { }
    
    protected override Role MapToEntity(RoleModel model)
    {
        return new Role
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    protected override RoleModel MapToModel(Role entity)
    {
        return new RoleModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}