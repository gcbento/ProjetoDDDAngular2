using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Admin2.Domain.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Admin2.Data.Repositories
{
    internal class CompraRepository : RepositoryBase, ICompraRepository
    {
        public CompraRepository(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public Compra Save(Compra model)
        {
            model.Id = connection.QueryFirst<int>("Exec saveCompra @id, @descricao, @valor", model);
            return model;
        }

        public bool Delete(int id)
        {
            var delete = connection.Execute("Exec deleteCompra @id", new { id = id});
            return delete > 0;
        }

        public Compra GetById(int id)
        {
            var result = connection.QueryFirstOrDefault<Compra>("Exec get @id", id);
            return result;
        }

        public IEnumerable<Compra> List(CompraFilter filter)
        {
            var result = connection.Query<Compra>("Exec GetListCompra @text", filter);
            return result;
        }
    }
}
