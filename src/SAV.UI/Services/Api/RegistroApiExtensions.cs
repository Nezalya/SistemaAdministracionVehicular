namespace SAV.UI.Services.Api;

public static class RegistroApiExtensions
{
    /// <summary>
    /// Registra todos los clientes tipados de la API. Un cliente por recurso:
    /// las pantallas nunca hablan con HttpClient directamente.
    /// </summary>
    public static IServiceCollection AddApiClients(
        this IServiceCollection services, IConfiguration config, IWebHostEnvironment entorno)
    {
        var baseUrl = config["Api:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Falta la configuración 'Api:BaseUrl' en appsettings.json.");

        var segundos = config.GetValue<int?>("Api:TiempoEsperaSegundos") ?? 30;

#if DEBUG
        // Válvula de escape para certificados de desarrollo no confiados.
        // Queda fuera de la compilación de Release: el binario que se despliega
        // no contiene siquiera la posibilidad de saltarse la validación TLS.
        var ignorarCertificado = entorno.IsDevelopment()
            && config.GetValue<bool>("Api:IgnorarErroresDeCertificado");
#else
        const bool ignorarCertificado = false;
#endif

        void Configurar(HttpClient cliente)
        {
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.Timeout = TimeSpan.FromSeconds(segundos);
        }

        services.AddHttpClient<IAuthApi, AuthApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IReportesApi, ReportesApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IAutomovilApi, AutomovilApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IMarcaApi, MarcaApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IModeloApi, ModeloApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<ICaracteristicaApi, CaracteristicaApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IFabricaApi, FabricaApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IClienteApi, ClienteApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IVendedorApi, VendedorApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IServicioOficialApi, ServicioOficialApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IConcesionarioApi, ConcesionarioApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IUsuarioApi, UsuarioApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IVentaApi, VentaApi>(Configurar).ConfigurarCertificado(ignorarCertificado);

        return services;
    }

    private static IHttpClientBuilder ConfigurarCertificado(this IHttpClientBuilder builder, bool ignorar)
    {
        if (!ignorar) return builder;

        return builder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });
    }
}
