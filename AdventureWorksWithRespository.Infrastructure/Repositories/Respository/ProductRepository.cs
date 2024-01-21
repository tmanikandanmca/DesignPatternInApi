using AdventureWorks.Domain.Models;

namespace AdventureWorksWithRespository.Infrastructure.Repositories.Respository;

public class ProductRepository : GenricRepository<Product>
{
    public ProductRepository(AdventureWorksContext context) : base(context)
    {
    }

    public override Product Get(int id)
    {
        return context.Products.Where(e => e.ProductID == id).FirstOrDefault();
    }

    public override List<Product> GetAll()
    {
        var products = base.GetAll();
        return products.ToList();
    }

    public Product GetByName(string Name)
    {
        var res= base.Find(x=>x.Name == Name).FirstOrDefault();
        return res;
    }
}
