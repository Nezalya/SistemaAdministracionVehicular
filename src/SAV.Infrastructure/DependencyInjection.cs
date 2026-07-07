using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAV.Infrastructure.Data;
using SAV.Infrastructure.Persistence.Repositories;

namespace SAV.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration config)
        {
            // DbContext
            services.AddDbContext<SavDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            services.AddScoped<ICaracteristicaRepository, CaracteristicaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFabricaImportService, FabricaImportService>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();
            services.AddScoped<IServicioOficialRepository, ServicioOficialRepository>();
            services.AddScoped<IAutomovilRepository, AutomovilRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IConcesionarioRepository, ConcesionarioRepository>();
            return services;
        }
    }
}
