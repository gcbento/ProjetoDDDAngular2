using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class Conta
    {
        public Conta()
        {
            TipoConta = new TipoConta();
            Jogos = new List<Compra>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string IdOnline { get; set; }
        public TipoConta TipoConta { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataDesativacao { get; set; }

        public List<Compra> Jogos { get; set; }
        public List<CodigosConta> Codigos { get; set; }
    }
}
