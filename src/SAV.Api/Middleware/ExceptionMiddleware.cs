namespace SAV.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcionAsync(context, ex);
            }
        }

        private async Task ManejarExcepcionAsync(HttpContext context, Exception ex)
        {
            var codigo = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,        
                BusinessException => (int)HttpStatusCode.BadRequest,      
                _ => (int)HttpStatusCode.InternalServerError  
            };

           
            if (codigo == (int)HttpStatusCode.InternalServerError)
                _logger.LogError(ex, "Error no controlado en {Path}", context.Request.Path);

            var mensaje = codigo == (int)HttpStatusCode.InternalServerError
                ? "Ocurrió un error interno en el servidor."
                : ex.Message;

            context.Response.StatusCode = codigo;

            var respuesta = new RespuestaApi<object>
            {
                Exito = false,
                Mensaje = mensaje,
                Datos = null
            };

            var opciones = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsJsonAsync(respuesta, opciones);
        }
    }
}