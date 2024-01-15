using AdventureWorks.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SqlesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _service;
    public CurrencyController(ICurrencyService service)
    {
        _service = service;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAllResult()
    {
        return Ok(_service.GetAllCurrencies());
    }

}
