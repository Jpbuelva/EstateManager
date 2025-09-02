using EstateManager.Application.Interfaces;
using EstateManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EstateManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPropertyService, PropertyService>();
        // si en el futuro hay más servicios, se registran aquí

        return services;
    }
}
