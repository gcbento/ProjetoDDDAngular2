using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class SaldoMensal
    {
        public int NumeroMes { get; set; }
        public string Mes { get; set; }
        public decimal ValorVendas { get; set; }
        public decimal ValorCompras { get; set; }
        public decimal Saldo { get; set; }
    }
}
