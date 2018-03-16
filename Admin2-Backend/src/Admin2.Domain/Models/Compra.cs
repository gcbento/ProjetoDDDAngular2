using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class Compra
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public DateTime DataCompra { get; set; }
    }
}
