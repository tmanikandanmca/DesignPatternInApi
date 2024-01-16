using AdventureWorks.Domain.Models;

namespace AdventureWorksWithRpository.Application.ServicesInterfaces;

public interface IProductService
{
    List<Product> GetProducts();
    Product GetByName(string name);
    Product GetById(int id);
}
