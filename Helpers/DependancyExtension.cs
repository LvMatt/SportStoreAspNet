using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Helpers
{
    public static class DependancyExtension
    {
        public static IServiceCollection AddCloudscribeCore(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, SqlProductRepository>();
            services.AddScoped<ICustomerRepository, SqlCustomersRepository>();

            return services;
        }
    }
}
