using AdventureWorks.Application.Repository.Repositories;
using AdventureWorks.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection ImplementInfrastructure(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<AdventureWorksContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("ShoppingCart")));
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        return services;
    }

}
