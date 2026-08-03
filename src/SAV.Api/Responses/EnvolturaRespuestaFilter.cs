namespace SAV.Api.Responses
{
    public class EnvolturaRespuestaFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var codigo = objectResult.StatusCode ?? StatusCodes.Status200OK;
                var exito = codigo is >= 200 and < 300;

                objectResult.Value = exito
                    ? new RespuestaApi<object> { Exito = true, Datos = objectResult.Value }
                    : new RespuestaApi<object>
                    {
                        Exito = false,
                        Mensaje = ExtraerMensaje(objectResult.Value),
                        Errores = (objectResult.Value as ValidationProblemDetails)?.Errors
                    };
            }


            await next();
        }

        private static string ExtraerMensaje(object? valor) => valor switch
        {
            ValidationProblemDetails => "Hay errores de validación en los datos enviados.",
            ProblemDetails p => p.Detail ?? p.Title ?? "Error en la petición.",
            string s => s,
            null => "Error en la petición.",
            _ => valor.ToString() ?? "Error en la petición."
        };
    }

}
