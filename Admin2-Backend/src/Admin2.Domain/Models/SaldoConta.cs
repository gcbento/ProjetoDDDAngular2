using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class SaldoConta
    {
        public SaldoConta()
        {
            Conta = new Conta();
        }

        public Conta Conta { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal Saldo { get; set; }
    }   
}
