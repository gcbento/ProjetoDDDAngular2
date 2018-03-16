using Admin2.Data.Repositories;
using Admin2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Data.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(ICompraRepository), typeof(CompraRepository));
            dic.Add(typeof(IContaRepository), typeof(ContaRepository));
            dic.Add(typeof(IClienteRepository), typeof(ClienteRepository));
            dic.Add(typeof(IVendaRepository), typeof(VendaRepository));
            dic.Add(typeof(ISaldoRepository), typeof(SaldoRepository));

            return dic;
        }
    }
}
