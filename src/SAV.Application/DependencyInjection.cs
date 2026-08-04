using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SAV.Application.Services;

namespace SAV.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMarcaService, MarcaService>();
        services.AddScoped<IAutomovilService, AutomovilService>();
        services.AddScoped<IVentaService, VentaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IModeloService, ModeloService>();
        services.AddScoped<ICaracteristicaService, CaracteristicaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IVendedorService, VendedorService>();
        services.AddScoped<IConcesionarioService, ConcesionarioService>();
        services.AddScoped<IServicioOficialService, ServicioOficialService>();
        services.AddScoped<IReporteService, ReporteService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<PasswordHasher<Usuario>>();

        return services;
    }
}