using AdventureWorks.Application.Repository.Repositories;
using AdventureWorks.Domain.Models;

namespace AdventureWorks.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly AdventureWorksContext _context;
    public CurrencyRepository(AdventureWorksContext context)
    {
        _context = context;
    }
    public bool Add(Currency entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Currency> GetAll()
    {
        return _context.Currencies.ToList();
    }

    public Currency GetById(int id)
    {
        return _context.Currencies.Where(e => e.CurrencyCode == id.ToString()).FirstOrDefault();
    }

    public bool Update(Currency entity)
    {
        throw new NotImplementedException();
    }
}
