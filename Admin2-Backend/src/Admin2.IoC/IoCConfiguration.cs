
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.IoC
{
    public static class IoCConfiguration
    {
        public static void Configuration(IServiceCollection services)
        {
            Configure(services, AppServices.IoC.Module.GetTypes());
            Configure(services, Data.IoC.Module.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}
