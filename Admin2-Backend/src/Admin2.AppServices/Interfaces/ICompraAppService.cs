using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Interfaces
{
    public interface ICompraAppService
    {
        GenericResult<Compra> Save(Compra model);
        GenericResult<IEnumerable<Compra>> List(CompraFilter filter);
        GenericResult<Compra> GetById(int id);
        GenericResult<bool> Delete(int id);
    }
}
