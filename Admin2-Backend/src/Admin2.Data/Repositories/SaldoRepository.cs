using Admin2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin2.Domain.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using Admin2.Domain.Filters;
using System.Dynamic;

namespace Admin2.Data.Repositories
{
    public class SaldoRepository : RepositoryBase, ISaldoRepository
    {
        private readonly ContaRepository contaRep;

        public SaldoRepository(IConfigurationRoot configuration) : base(configuration)
        {
            contaRep = new ContaRepository(configuration);
        }

        public Saldo GetSaldo(SaldoFilter filter)
        {
            Saldo saldo = new Saldo();

            saldo.Estatisticas = GetEstatisticas(filter);
            saldo.SaldoContas = ListSaldoContas(filter).ToList();

            return saldo;
        }

        public Estatisticas GetEstatisticas(SaldoFilter filter)
        {
            var result = connection.QueryFirstOrDefault<Estatisticas>("Exec GetEstatisticas @todasConta, @text", filter);
            return result;
        }

        public IEnumerable<SaldoConta> ListSaldoContas(SaldoFilter filter)
        {
            var result = connection.Query("Exec GetListSaldoConta @todasConta, @text", filter);
            var listSaldoContas = new List<SaldoConta>();

            foreach (var row in result)
            {
                SaldoConta saldo = new SaldoConta();

                saldo.Conta = contaRep.GetById(row.contaId);
                saldo.TotalVendido = row.totalVendido;
                saldo.Saldo = row.saldo;

                listSaldoContas.Add(saldo);
            }

            return listSaldoContas;
        }

        public IEnumerable<Estatisticas> GetSaldoMes()
        {
            var result = connection.Query<Estatisticas>("Exec SaldoMes");
            return result;
        }

        public IEnumerable<Estatisticas> GetSaldoMensal(int ano)
        {
            var result = connection.Query<Estatisticas>("Exec SaldoMensal @ano", new { ano = ano });
            return result;
        }
    }
}
