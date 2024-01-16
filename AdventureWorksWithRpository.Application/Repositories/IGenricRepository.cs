using System.Linq.Expressions;

namespace AdventureWorksWithRpository.Application.Repositories;

public interface IGenricRepository<T>
{
    T Add(T Entity);
    T Update(T Entity);
    T Get(int  id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T,bool>> predicate);
    void SaveChanges();

}
