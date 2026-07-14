namespace SAV.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient<IMarcaService, MarcaService>();
        services.AddScoped<IAutomovilService, AutomovilService>();
        services.AddScoped<IVentaService, VentaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IModeloService, ModeloService>();
        services.AddScoped<ICaracteristicaService, CaracteristicaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IVendedorService, VendedorService>();
        services.AddScoped<IConcesionarioService, ConcesionarioService>();
        services.AddScoped<IServicioOficialService, ServicioOficialService>();
        return services;
    }

}


