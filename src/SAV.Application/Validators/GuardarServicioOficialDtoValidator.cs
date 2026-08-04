using FluentValidation;

namespace SAV.Application.Validators
{
    public class GuardarServicioOficialDtoValidator : AbstractValidator<GuardarServicioOficialDto>
    {
        public GuardarServicioOficialDtoValidator()
        {
            RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$")
                .WithMessage("El nombre solo puede contener letras y espacios.");

            RuleFor(x => x.Nif)
            .NotEmpty().WithMessage("El NIF es obligatorio.")
            .Length(11).WithMessage("La cédula debe contener exactamente 11 dígitos.")
            .Matches(@"^\d+$").WithMessage("El NIF solo puede contener números.");


            RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Length(10).WithMessage("El teléfono debe contener exactamente 10 dígitos.")
                .Matches(@"^\d+$").WithMessage("El teléfono solo puede contener números.");
            RuleFor(x => x.Domicilio)
                .MaximumLength(200).WithMessage("El domicilio no puede exceder los 200 caracteres.")
                .Matches(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ .,#\-]+$")
                    .WithMessage("El domicilio contiene caracteres no permitidos.")
                .When(x => !string.IsNullOrWhiteSpace(x.Domicilio));
        }
    }
}
