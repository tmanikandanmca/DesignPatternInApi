using AdventureWorks.Application.ServicesInterfaces;
using Chapter_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Chapter_1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _service;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<CurrencyController> _logger;
    private IMemoryCache _cache;

    public CurrencyController(ICurrencyService service
        , IMemoryCache cache
        , ILoggerFactory loggerFactory
        , ILogger<CurrencyController> logger)
    {
        _loggerFactory = loggerFactory;
        _cache= cache;
        _logger = _loggerFactory.CreateLogger<CurrencyController>();
        ICurrencyService _cwithLogger = new CurrencyWithLogger(_loggerFactory.CreateLogger<CurrencyWithLogger>(), service);
        ICurrencyService _cwithCache = new CurrencyWithCache(_cwithLogger, cache);

        _service = _cwithCache;
    }


    [HttpGet("GetAll")]
    public IActionResult GetAllResult()
    {
        return Ok(_service.GetAllCurrencies());
    }

    [HttpGet("GetById{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_service.GetCurrencyById(id));
    }



}
