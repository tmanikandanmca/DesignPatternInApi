using AdventureWorksWithRpository.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksWithRespository.Infrastructure.Repositories;

public abstract class GenricRepository<T> : IGenricRepository<T> where T : class
{
    protected readonly AdventureWorksContext context;
    public GenricRepository(AdventureWorksContext context)
    {
        this.context = context; 
    }
    public virtual T Add(T Entity)
    {
       return context.Add(Entity).Entity;
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToList();

    }

    public virtual T Get(int id)
    {
      return context.Find<T>(id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public virtual void SaveChanges()
    {
        context.SaveChanges();
    }

    public virtual T Update(T Entity)
    {
        return context.Update(Entity).Entity;
    }
}
