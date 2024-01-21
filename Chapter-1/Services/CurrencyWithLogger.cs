using AdventureWorks.Application.ServicesInterfaces;
using AdventureWorks.Domain.Models;

namespace Chapter_1.Services;

public class CurrencyWithLogger(ILogger<CurrencyWithLogger> logger, ICurrencyService currencyService) : ICurrencyService
{
    private readonly ILogger<CurrencyWithLogger> _logger = logger;
    private readonly ICurrencyService _currencyService = currencyService;

    public bool AddCurrency(Currency currency)
    {
        throw new NotImplementedException();
    }

    public bool DeleteCurrency(int id)
    {
        throw new NotImplementedException();
    }

    public List<Currency> GetAllCurrencies()
    {
        _logger.LogTrace("get from api");
        return _currencyService.GetAllCurrencies();
    }

    public Currency GetCurrencyById(int id)
    {
        _logger.LogTrace("get by id from api");
        return _currencyService.GetCurrencyById(id);
    }

    public Currency GetCurrencyByName(string name)
    {
        throw new NotImplementedException();
    }

    public bool UpdateCurrency(Currency currency)
    {
        throw new NotImplementedException();
    }
}
