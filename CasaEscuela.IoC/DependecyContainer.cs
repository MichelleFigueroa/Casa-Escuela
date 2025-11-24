
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CasaEscuela.DAL;
namespace CasaEscuela.IoC
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddIoCDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDALDependecies(configuration);
            return services;
        }
    }
}
