using AdventureWorksWithRpository.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryWorks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly IProductService service;
    public ProductsController(IProductService service)
	{
			this.service = service;
	}

	[HttpGet]
	public IActionResult GetAllProducts()
	{
		return Ok(service.GetProducts());

    }

    [HttpGet("GetById{id}")]
    public IActionResult GetProductsById(int Id)
    {
        return Ok(service.GetById(Id));

    }
}
