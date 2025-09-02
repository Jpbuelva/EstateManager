using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EstateManager.Domain.Entities;

namespace EstateManager.Infrastructure.Persistence
{
    public class EstateDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public EstateDbContext(DbContextOptions<EstateDbContext> options) : base(options) { }

        // Tus entidades del dominio
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignorar tablas que no necesitas
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();

            // Aplicar configuraciones de tus entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstateDbContext).Assembly);
        }
    }
}
