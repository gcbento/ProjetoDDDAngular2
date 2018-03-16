using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin2.Domain.Models;
using Admin2.Domain.Filters;

namespace Admin2.Domain.Repositories
{
    public interface ISaldoRepository
    {
        Estatisticas GetEstatisticas(SaldoFilter filter);
        IEnumerable<SaldoConta> ListSaldoContas(SaldoFilter filter);
        IEnumerable<Estatisticas> GetSaldoMes();
        IEnumerable<Estatisticas> GetSaldoMensal(int ano);
    }
}
