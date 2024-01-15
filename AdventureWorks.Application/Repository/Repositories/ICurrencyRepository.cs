using AdventureWorks.Domain.Models;

namespace AdventureWorks.Application.Repository.Repositories;

public interface ICurrencyRepository
{
    IEnumerable<Currency> GetAll();
    Currency GetById(int id);
    bool Add(Currency entity);
    bool Update(Currency entity);
    bool Delete(int id);
}
