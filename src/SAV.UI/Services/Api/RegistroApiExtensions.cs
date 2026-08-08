namespace SAV.UI.Services.Api;

public static class RegistroApiExtensions
{
    /// <summary>
    /// Registra todos los clientes tipados de la API. Cada fase añade aquí los suyos.
    /// </summary>
    public static IServiceCollection AddApiClients(
        this IServiceCollection services, IConfiguration config, IWebHostEnvironment entorno)
    {
        var baseUrl = config["Api:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Falta la configuración 'Api:BaseUrl' en appsettings.json.");

        var segundos = config.GetValue<int?>("Api:TiempoEsperaSegundos") ?? 30;

        // Solo se permite saltarse la validación del certificado en desarrollo.
        var ignorarCertificado = entorno.IsDevelopment()
            && config.GetValue<bool>("Api:IgnorarErroresDeCertificado");

        services.AddHttpClient<IAuthApi, AuthApi>(Configurar).ConfigurarCertificado(ignorarCertificado);
        services.AddHttpClient<IReportesApi, ReportesApi>(Configurar).ConfigurarCertificado(ignorarCertificado);

        return services;

        void Configurar(HttpClient cliente)
        {
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.Timeout = TimeSpan.FromSeconds(segundos);
        }
    }

    private static IHttpClientBuilder ConfigurarCertificado(
        this IHttpClientBuilder builder, bool ignorar)
    {
        if (!ignorar) return builder;

        return builder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });
    }
}