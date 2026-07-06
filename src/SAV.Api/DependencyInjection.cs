using SAV.Core.Interfaces.Repositories;
using SAV.Core.Interfaces.Services;
using SAV.Infrastructure.Data;
using SAV.Infrastructure.Persistence.Repositories;

namespace SAV.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IFabricaImportService, FabricaImportService>();
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        return services;
    }


}


