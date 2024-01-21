using AdventureWorks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Infrastructure.Repositories;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int id);
}
