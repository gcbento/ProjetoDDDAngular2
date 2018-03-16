using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Admin2.Data.Repositories
{
    public class RepositoryBase
    {
        private const string CONNECTION_STRING = "ConnectionString";

        protected SqlConnection connection;

        public RepositoryBase(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection(CONNECTION_STRING);
            if (string.IsNullOrWhiteSpace(connectionString.Value))
                throw new ArgumentNullException("Connection string não encontrada");

            connection = new SqlConnection(connectionString.Value);
        }
    }
}
