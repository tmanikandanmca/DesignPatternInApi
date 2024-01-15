using AdventureWorks.Application.Repository.Repositories;
using AdventureWorks.Application.ServicesInterfaces;
using AdventureWorks.Domain.Models;

namespace AdventureWorks.Application.Services;

public class CurrencyService : ICurrencyService
{

    private readonly ICurrencyRepository _repo;
    public CurrencyService(ICurrencyRepository repo)
    {
        _repo=repo;
    }
    public bool AddCurrency(Currency currency)
    {
       return _repo.Add(currency);
        
    }

    public bool DeleteCurrency(int id)
    {
      return _repo.Delete(id);
    }

    public List<Currency> GetAllCurrencies()
    {
        return _repo.GetAll().ToList();
    }

    public Currency GetCurrencyById(int id)
    {
        return _repo.GetById(id);

    }

    public Currency GetCurrencyByName(string name)
    {
        throw new NotImplementedException();
    }

    public bool UpdateCurrency(Currency currency)
    {
       return _repo.Update(currency);
    }
}
