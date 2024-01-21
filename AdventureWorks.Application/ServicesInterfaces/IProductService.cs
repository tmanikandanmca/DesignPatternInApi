namespace AdventureWorks.Application.ServicesInterfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProducts();

    Task<ProductDTO> GetProductsById(int id);
}

public record ProductDTO
{
    public int productId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int unitPrice {  get; set; }
    public string categoryId {  get; set; }
    public string category { get; set; }
}

