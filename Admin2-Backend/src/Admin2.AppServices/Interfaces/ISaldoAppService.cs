using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Interfaces
{
    public interface ISaldoAppService
    {
        GenericResult<Estatisticas> GetEstatisticas(SaldoFilter filter);
        GenericResult<IEnumerable<SaldoConta>> ListSaldoContas(SaldoFilter filter);
        GenericResult<IEnumerable<Estatisticas>> GetSaldoMes();
        GenericResult<IEnumerable<Estatisticas>> GetSaldoMensal(int ano);
    }
}
