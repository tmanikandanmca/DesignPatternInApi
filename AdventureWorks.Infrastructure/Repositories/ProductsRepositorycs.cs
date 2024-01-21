using AdventureWorks.Domain.Models;

namespace AdventureWorks.Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly AdventureWorksContext context;

    public ProductsRepository(AdventureWorksContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return context.Products.ToList();
    }

    public async Task<Product> GetById(int id)
    {
        return context.Products.Where(e => e.ProductID == id).FirstOrDefault();
    }
}
