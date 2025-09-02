using EstateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateManager.Infrastructure.Persistence;

public class EstateDbContext : DbContext
{
    public EstateDbContext(DbContextOptions<EstateDbContext> options) : base(options) { }

    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<PropertyTrace> PropertyTraces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstateDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
