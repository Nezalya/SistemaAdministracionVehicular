using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using SAV.Contracts.Common;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

/// <summary>
/// Base de todos los clientes de la API. Centraliza en un único sitio: el token,
/// la desenvoltura de <c>RespuestaApi&lt;T&gt;</c>, los tiempos de espera y los errores.
/// Ninguna pantalla debe hablar con HttpClient directamente.
/// </summary>
public abstract class ApiClienteBase
{
    private static readonly JsonSerializerOptions OpcionesJson = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly HttpClient _http;
    private readonly ITokenAccessor _tokenAccessor;
    private readonly ILogger _logger;

    protected ApiClienteBase(HttpClient http, ITokenAccessor tokenAccessor, ILogger logger)
    {
        _http = http;
        _tokenAccessor = tokenAccessor;
        _logger = logger;
    }

    protected Task<ResultadoApi<T>> GetAsync<T>(string ruta, CancellationToken ct = default)
        => EnviarAsync<T>(HttpMethod.Get, ruta, null, ct);

    protected Task<ResultadoApi<T>> PostAsync<T>(string ruta, object? cuerpo, CancellationToken ct = default)
        => EnviarAsync<T>(HttpMethod.Post, ruta, cuerpo, ct);

    protected Task<ResultadoApi<T>> PutAsync<T>(string ruta, object? cuerpo, CancellationToken ct = default)
        => EnviarAsync<T>(HttpMethod.Put, ruta, cuerpo, ct);

    protected Task<ResultadoApi<T>> DeleteAsync<T>(string ruta, CancellationToken ct = default)
        => EnviarAsync<T>(HttpMethod.Delete, ruta, null, ct);

    private async Task<ResultadoApi<T>> EnviarAsync<T>(
        HttpMethod metodo, string ruta, object? cuerpo, CancellationToken ct)
    {
        try
        {
            using var peticion = new HttpRequestMessage(metodo, ruta);

            if (cuerpo is not null)
                peticion.Content = JsonContent.Create(cuerpo, options: OpcionesJson);

            var token = await _tokenAccessor.ObtenerTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
                peticion.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var respuesta = await _http.SendAsync(peticion, ct);
            var codigo = (int)respuesta.StatusCode;

            // Los DELETE de la API devuelven 204 sin cuerpo y sin envoltura.
            if (respuesta.StatusCode == HttpStatusCode.NoContent)
                return ResultadoApi<T>.Ok(default, codigo);

            var sobre = await LeerSobreAsync<T>(respuesta, ct);

            if (respuesta.IsSuccessStatusCode)
                return ResultadoApi<T>.Ok(sobre is not null ? sobre.Datos : default, codigo);

            return ResultadoApi<T>.Fallo(
                sobre?.Mensaje ?? MensajePorDefecto(codigo), codigo, sobre?.Errores);
        }
        catch (TaskCanceledException) when (!ct.IsCancellationRequested)
        {
            _logger.LogWarning("Tiempo de espera agotado en {Metodo} {Ruta}", metodo, ruta);
            return ResultadoApi<T>.Fallo(
                "El servidor tardó demasiado en responder. Inténtalo de nuevo.", 408);
        }
        catch (OperationCanceledException)
        {
            throw; // Cancelación real (el usuario navegó a otra pantalla): no es un error.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fallo al llamar a {Metodo} {Ruta}", metodo, ruta);
            return ResultadoApi<T>.Fallo(
                "No se pudo contactar con el servidor. Comprueba que la API esté en marcha.", 0);
        }
    }

    private static async Task<RespuestaApi<T>?> LeerSobreAsync<T>(
        HttpResponseMessage respuesta, CancellationToken ct)
    {
        // Un 401 emitido por el middleware JWT llega sin cuerpo: no hay envoltura que leer.
        if (respuesta.Content.Headers.ContentType?.MediaType != "application/json")
            return null;

        try
        {
            return await respuesta.Content.ReadFromJsonAsync<RespuestaApi<T>>(OpcionesJson, ct);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string MensajePorDefecto(int codigo) => codigo switch
    {
        400 => "Los datos enviados no son válidos.",
        401 => "Tu sesión ha caducado. Vuelve a iniciar sesión.",
        403 => "No tienes permiso para realizar esta acción.",
        404 => "No se encontró el recurso solicitado.",
        409 => "La operación entra en conflicto con el estado actual de los datos.",
        _ => "Se produjo un error inesperado en el servidor."
    };
}