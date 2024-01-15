using AdventureWorks.Domain.Models;

namespace AdventureWorks.Application.ServicesInterfaces;

public interface ICurrencyService
{
    List<Currency> GetAllCurrencies();
    Currency GetCurrencyById(int id);
    Currency GetCurrencyByName(string name);
    bool AddCurrency(Currency currency);
    bool UpdateCurrency(Currency currency);
    bool DeleteCurrency(int id);
}
