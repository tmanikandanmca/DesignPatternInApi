using AdventureWorks.Domain.Models;
using AdventureWorksWithRespository.Infrastructure.Repositories;
using AdventureWorksWithRespository.Infrastructure.Repositories.Respository;
using AdventureWorksWithRpository.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorksWithRespository.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection  services, IConfiguration Configuration)
    {
        services.AddDbContext<AdventureWorksContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("ShoppingCart")));
        services.AddTransient<IGenricRepository<Product>,ProductRepository>();
        return services;
    }

}
