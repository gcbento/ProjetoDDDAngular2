using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class Estatisticas
    {
        public int NumeroMes { get; set; }
        public string Mes { get; set; }
        public decimal ValorTotalVendas { get; set; }
        public int QtdeTotalVendas { get; set; }
        public int QtdeVendas { get; set; }
        public decimal ValorVendas { get; set; }
        public int QtdeAlugueis { get; set; }
        public decimal ValorAlugueis { get; set; }
        public decimal ValorTotalCompras { get; set; }
        public int QtdeTotalCompras { get; set; }
        public decimal Saldo { get; set; }
    }
}
