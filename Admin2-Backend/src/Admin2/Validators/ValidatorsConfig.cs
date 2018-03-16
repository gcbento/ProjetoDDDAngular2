using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Validators
{
    public static class ValidatorsConfig
    {
        public static void AddToScope(IServiceCollection services)
        {
            services.AddScoped<CompraValidator>();
            services.AddScoped<ContaValidator>();
            services.AddScoped<ClienteValidator>();
            services.AddScoped<VendaValidator>();
        }
    }
}
