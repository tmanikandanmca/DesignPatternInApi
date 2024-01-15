using AdventureWorks.Application.Services;
using AdventureWorks.Application.ServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyService, CurrencyService>();
        return services;
    }
}
