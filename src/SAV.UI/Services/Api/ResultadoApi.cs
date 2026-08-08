namespace SAV.UI.Services.Api;

/// <summary>
/// Resultado de una llamada a la API, ya desenvuelto de <c>RespuestaApi&lt;T&gt;</c>.
/// Nunca lanza excepciones por errores HTTP: los errores son parte del resultado.
/// </summary>
public sealed class ResultadoApi<T>
{
    public bool Exito { get; init; }
    public T? Datos { get; init; }
    public string? Mensaje { get; init; }

    /// <summary>Errores de validación por campo, tal y como los devuelve FluentValidation en la API.</summary>
    public IDictionary<string, string[]>? Errores { get; init; }

    /// <summary>Código HTTP. Vale 0 cuando ni siquiera se pudo contactar con el servidor.</summary>
    public int CodigoEstado { get; init; }

    public bool NoAutenticado => CodigoEstado == 401;
    public bool SinPermiso => CodigoEstado == 403;
    public bool NoEncontrado => CodigoEstado == 404;
    public bool SinConexion => CodigoEstado == 0;
    public bool HayErroresDeValidacion => Errores is { Count: > 0 };

    public static ResultadoApi<T> Ok(T? datos, int codigo) =>
        new() { Exito = true, Datos = datos, CodigoEstado = codigo };

    public static ResultadoApi<T> Fallo(string mensaje, int codigo,
                                        IDictionary<string, string[]>? errores = null) =>
        new() { Exito = false, Mensaje = mensaje, CodigoEstado = codigo, Errores = errores };
}