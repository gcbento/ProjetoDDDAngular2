using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Interfaces
{
    public interface IClienteAppService
    {
        GenericResult<Cliente> Save(Cliente model);
        GenericResult<IEnumerable<Cliente>> List(ClienteFilter filter);
        GenericResult<IEnumerable<SelectModel>> GetSelectClientes();
        GenericResult<Cliente> GetById(int id);
        GenericResult<bool> Delete(int id);
    }
}
