using Microsoft.OpenApi;
using System.Reflection;

namespace SAV.Api.Extensions
{
    public static class DocumentationExtensions
    {
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)//Metodo de extension para poner simplemente en el program builder.Services.AddApiDocumentation();
        {
            services.AddEndpointsApiExplorer();//Explora todos los endpoints de la API y genera documentación para ellos

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo//Esta informacion aparecera arriba de Swagger UI.
                {
                    Title = "SAV — Sistema de Administración Vehicular",
                    Version = "v1",
                    Description = "API para la gestión de un concesionario de automóviles: " +
                                  "catálogo de vehículos, inventario por número de bastidor, " +
                                  "red de distribución, personal de ventas, clientes y registro " +
                                  "de ventas con extras."
                });

                // Comentarios XML del código
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath);

                // Botón "Authorize" para el JWT
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Token JWT obtenido en POST /api/auth/login"
                });

                // Aplica el esquema a los endpoints (para que el token SÍ se envíe)
                options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
                {
                    { new OpenApiSecuritySchemeReference("Bearer",doc), new List<string>() }
                });
                options.OperationFilter<RespuestasSeguridadOperationFilter>();
            });
            return services;
        }
    }
}