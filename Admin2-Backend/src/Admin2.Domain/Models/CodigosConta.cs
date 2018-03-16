using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Models
{
    public class CodigosConta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public int ContaId { get; set; }
    }
}
