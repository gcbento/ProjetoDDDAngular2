using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class ItemVenda
    {
        public ItemVenda()
        {
            Venda = new Venda();
            Conta = new Conta();
            TipoVenda = new TipoVenda();
            TipoConta = new TipoConta();
        }

        public int Id { get; set; }
        public Venda Venda { get; set; }
        public Conta Conta { get; set; }
        public TipoVenda TipoVenda { get; set; }
        public decimal Valor { get; set; }
        public TipoConta TipoConta { get; set; }
        public int qtdeDias { get; set; }
        public DateTime InicioLocacao { get; set; }
        public DateTime FimLocacao { get; set; }
        public bool DiasGratis { get; set; }
        public string Senha { get; set; }
        public string TipoPeriodo { get; set; }
    }
}
