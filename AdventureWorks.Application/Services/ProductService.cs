using AdventureWorks.Application.ServicesInterfaces;
using AdventureWorks.Domain.Models;
using AdventureWorks.Infrastructure.Repositories;

namespace AdventureWorks.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductsRepository repo;
    public ProductService(IProductsRepository repo)
    {
        this.repo = repo;
    }
    public Task<IEnumerable<ProductDTO>> GetProducts()
    {
       var res = repo.GetAll();
        //var response = repo.GetAll().Select(x => new ProductDTO { }).ToList();
        //    .Select(e=> new  ProductDTO {  
        //    ProductId = e.ProductId, 
        //    ProductName = e.ProductName,
        //    description=e.ProductNumber,
        //    unitPrice=e.ListPrice,
        //    categoryId=e.ProductSubcategoryID,
        //}).ToList();
        List<ProductDTO> products = [];
        throw new NotImplementedException();
    }

    public Task<ProductDTO> GetProductsById(int id)
    {
        throw new NotImplementedException();
    }
}
