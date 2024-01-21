using AdventureWorks.Domain.Models;
using AdventureWorksWithRpository.Application.Repositories;
using AdventureWorksWithRpository.Application.ServicesInterfaces;

namespace AdventureWorksWithRpository.Application.Services;
internal class ProductService : IProductService
{

    private readonly IGenricRepository<Product> repo;
    public ProductService(IGenricRepository<Product> repo)
    {
        this.repo = repo;
    }
    public Product GetById(int id)
    {
        return repo.Get(id);
    }

    public Product GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProducts()
    {
        return repo.GetAll().ToList();
    }
}
