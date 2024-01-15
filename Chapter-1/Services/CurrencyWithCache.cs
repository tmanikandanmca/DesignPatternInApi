using AdventureWorks.Application.ServicesInterfaces;
using AdventureWorks.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Chapter_1.Services;

public class CurrencyWithCache : ICurrencyService
{
    private readonly ICurrencyService _currencyService;
    private IMemoryCache _cache;
    public CurrencyWithCache(ICurrencyService currencyService, IMemoryCache cache)
    {
        _currencyService = currencyService;
        _cache = cache;
    }


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
        string cacheKey = $"GetCurrencyById::All";
        if (_cache.TryGetValue<List<Currency>>(cacheKey, out var values))
        {
            return values;
        }
        else
        {
            var res = _currencyService.GetAllCurrencies();
            _cache.Set<List<Currency>>(cacheKey, res, TimeSpan.FromMinutes(30));
            return res;
        }
    }

    public Currency GetCurrencyById(int id)
    {
        string cacheKey = $"GetCurrencyById::{id}";
        if (_cache.TryGetValue<Currency>(cacheKey, out var values))
        {
            return values;
        }
        else
        {
            var res = _currencyService.GetCurrencyById(id);
            _cache.Set<Currency>(cacheKey, res, TimeSpan.FromMinutes(30));
            return res;
        }

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
