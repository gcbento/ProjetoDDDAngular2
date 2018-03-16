using Admin2.AppServices.AppServices;
using Admin2.AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(ICompraAppService), typeof(CompraAppService));
            dic.Add(typeof(IContaAppService), typeof(ContaAppService));
            dic.Add(typeof(IClienteAppService), typeof(ClienteAppService));
            dic.Add(typeof(IVendaAppService), typeof(VendaAppService));
            dic.Add(typeof(ISaldoAppService), typeof(SaldoAppService));

            return dic;
        }
    }
}
