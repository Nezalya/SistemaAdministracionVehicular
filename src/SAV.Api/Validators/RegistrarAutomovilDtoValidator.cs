using FluentValidation;

namespace SAV.Core.Validators
{
    public class RegistrarAutomovilDtoValidator : AbstractValidator<RegistrarAutomovilDto>
    {
        public RegistrarAutomovilDtoValidator()
        {
            RuleFor(x => x.ModeloId).GreaterThan(0).WithMessage("Debe indicar un modelo válido.");

            RuleFor(x => x.NumBastidor)
                .NotEmpty().WithMessage("El número de bastidor es obligatorio.")
                .MaximumLength(17).WithMessage("El número de bastidor no puede exceder los 17 caracteres.");

            RuleFor(x => x.AnoFabricacion)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .When(x => x.AnoFabricacion.HasValue)
                .WithMessage("El año de fabricación no es válido.");

            RuleFor(x => x.Color).MaximumLength(20).WithMessage("El color no puede exceder los 20 caracteres.");

            // RN13 — no puede estar en dos ubicaciones a la vez
            RuleFor(x => x)
                .Must(a => !(a.ConcesionarioUbicacionId.HasValue && a.ServicioOficialUbicacionId.HasValue))
                .WithMessage("El automóvil no puede estar ubicado en dos lugares a la vez.");
        }
    }
}