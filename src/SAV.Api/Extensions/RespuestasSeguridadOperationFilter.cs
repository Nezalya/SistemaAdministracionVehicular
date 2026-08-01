using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SAV.Api.Extensions
{
    /// <summary>
    /// Agrega automáticamente las respuestas 401 y 403 a la documentación
    /// de todos los endpoints protegidos con [Authorize].
    /// </summary>
    public class RespuestasSeguridadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // ¿El endpoint (o su controller) exige autorización?
            var tieneAuthorize =
                (context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>().Any() ?? false)
                || context.MethodInfo.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>().Any();

            // ¿Está marcado como público?
            var permiteAnonimo = context.MethodInfo.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any();

            if (!tieneAuthorize || permiteAnonimo)
                return;

            operation.Responses.TryAdd("401", new OpenApiResponse
            {
                Description = "No autenticado. Falta el token JWT o es inválido."
            });

            operation.Responses.TryAdd("403", new OpenApiResponse
            {
                Description = "Autenticado pero sin el rol necesario para esta operación."
            });
        }
    }
}