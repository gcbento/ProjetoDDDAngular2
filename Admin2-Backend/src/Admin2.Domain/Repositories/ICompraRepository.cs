using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System.Collections.Generic;

namespace Admin2.Domain.Repositories
{
    public interface ICompraRepository
    {
        Compra Save(Compra model);
        IEnumerable<Compra> List(CompraFilter filter);
        Compra GetById(int id);
        bool Delete(int id);
    }
}
    