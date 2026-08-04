using FluentValidation;

namespace SAV.Application.Validators
{
    public class ActualizarAutomovilDtoValidator : AbstractValidator<ActualizarAutomovilDto>
    {
        public ActualizarAutomovilDtoValidator()
        {
            RuleFor(x => x.Color).MaximumLength(20).WithMessage("El color no puede exceder los 20 caracteres.");

            RuleFor(x => x.AnoFabricacion)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .When(x => x.AnoFabricacion.HasValue)
                .WithMessage("El año de fabricación no es válido.");

            // RN13 — no puede estar en dos ubicaciones a la vez
            RuleFor(x => x)
                .Must(a => !(a.ConcesionarioUbicacionId.HasValue && a.ServicioOficialUbicacionId.HasValue))
                .WithMessage("El automóvil no puede estar ubicado en dos lugares a la vez.");
        }
    }
}