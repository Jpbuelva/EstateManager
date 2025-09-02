using EstateManager.Domain.Abstractions;
using EstateManager.Infrastructure.Persistence;
using EstateManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstateManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 🔗 Configuración de EF Core con SQL Server
        services.AddDbContext<EstateDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositorios
        services.AddScoped<IPropertyRepository, PropertyRepository>(); 
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
