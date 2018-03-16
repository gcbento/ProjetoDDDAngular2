using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class Home
    {
        public List<ItemVenda> FimLocacoes { get; set; }
        public List<ItemVenda> ContasDisponiveis { get; set; }
        public List <Conta> ContasDesativar { get; set; }
        public List<Estatisticas> SaldoMes { get; set; }
    }
}
