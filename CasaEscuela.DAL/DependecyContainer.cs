using CasaEscuela.BL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaEscuela.DAL
{
    public static partial class DependecyContainer
    {
        public static IServiceCollection AddDALDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CasaEscuelaDBContext>(options =>
            options.UseMySql(configuration.GetConnectionString("Conn"), ServerVersion.AutoDetect(configuration.GetConnectionString("Conn"))));

            services.AddScoped<IUsuarioBL, UsuarioDAL>();
            services.AddScoped<IAnamnesisBL, AnamnesisDAL>();
            services.AddScoped<IPreceptoriaBL, PreceptoriaDAL>();
            services.AddScoped<ICatalogoBL, CatalogoDAL>();
            services.AddScoped<IPermisoBL, PermisoDAL>();
       
            return services;
        }
    }
}
