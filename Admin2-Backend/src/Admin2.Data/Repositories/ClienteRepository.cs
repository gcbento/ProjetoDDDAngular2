using Admin2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Admin2.Data.Repositories
{
    internal class ClienteRepository : RepositoryBase, IClienteRepository
    {
        public ClienteRepository(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public Cliente Save(Cliente model)
        {
            model.Id = connection.QueryFirst<int>("Exec saveCliente @id, @nome, @email", model);
            return model;
        }

        public IEnumerable<Cliente> List(ClienteFilter filter)
        {
            var result = connection.Query<Cliente>("Exec GetListCliente @text", filter);
            return result;
        }

        public Cliente GetById(int id)
        {
            var result = connection.QueryFirstOrDefault<Cliente>("Exec get @id", id);
            return result;
        }


        public bool Delete(int id)
        {
            var delete = connection.Execute("Exec deleteCliente @id", new { id = id });
            return delete > 0;
        }
    }
}
