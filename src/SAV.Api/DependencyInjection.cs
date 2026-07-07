namespace SAV.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient<IMarcaService, MarcaService>();
        return services;
    }

}


