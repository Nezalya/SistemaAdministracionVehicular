using FluentValidation;
using SAV.Api.Validators;
using SAV.Core.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace SAV.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddApiValidation(this IServiceCollection services)
        {
            
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
