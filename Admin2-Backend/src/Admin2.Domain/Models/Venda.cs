using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class Venda
    {
        public Venda()
        {
            Cliente = new Cliente();
            ItensVenda = new List<ItemVenda>();
        }

        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataVenda { get; set; }

        public List<ItemVenda> ItensVenda { get; set; }
    }
}
