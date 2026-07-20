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

                objectResult.Value = new RespuestaApi<object>
                {
                    Exito = exito,
                    Datos = exito ? objectResult.Value : null,
                    Mensaje = exito ? null : objectResult.Value?.ToString()
                };
            }

            await next();
        }
    }
}
