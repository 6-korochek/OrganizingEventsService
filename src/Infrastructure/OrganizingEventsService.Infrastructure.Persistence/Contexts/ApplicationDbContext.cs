#pragma warning disable SA1206

using Microsoft.EntityFrameworkCore;

namespace OrganizingEventsService.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public required DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Сюда добавлять различные конфигурации ваших файлов
        base.OnModelCreating(modelBuilder);
    }
}