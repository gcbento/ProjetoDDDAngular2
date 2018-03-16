using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Filters
{
    public class CompraFilter
    {
        public string Text { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }
    }
}
