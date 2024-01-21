using AdventureWorksWithRpository.Application.Services;
using AdventureWorksWithRpository.Application.ServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorksWithRpository.Application;

public static class AddApplicationService
{
    public static IServiceCollection ApplicationService(this IServiceCollection services)
    {
        services.AddTransient<IProductService,ProductService>();
        return services;
    }

}
