using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class Saldo
    {
        public Saldo()
        {
            Estatisticas = new Estatisticas();
            SaldoContas = new List<SaldoConta>();
        }

        public Estatisticas Estatisticas { get; set; }
        public List<SaldoConta> SaldoContas { get; set; }
    }
}
