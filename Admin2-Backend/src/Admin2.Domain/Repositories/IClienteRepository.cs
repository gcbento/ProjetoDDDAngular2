using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Repositories
{
    public interface IClienteRepository
    {
        Cliente Save(Cliente model);
        IEnumerable<Cliente> List(ClienteFilter filter);
        Cliente GetById(int id);
        bool Delete(int id);
    }
}
