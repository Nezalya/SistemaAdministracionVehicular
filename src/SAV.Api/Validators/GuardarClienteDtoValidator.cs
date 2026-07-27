using FluentValidation;

namespace SAV.Core.Validators
{
    public class GuardarClienteDtoValidator : AbstractValidator<GuardarClienteDto>
    {
        public GuardarClienteDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$")
                .WithMessage("El nombre solo puede contener letras y espacios.");

            RuleFor(x => x.Nif)
            .NotEmpty().WithMessage("El NIF es obligatorio.")
            .Length(11).WithMessage("La cédula debe contener exactamente 11 dígitos.")
            .Matches(@"^\d+$").WithMessage("La cédula solo puede contener números.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("El correo es obligatorio.").
                EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("El email no tiene un formato válido.")
                .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres."); ;

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Length(10).WithMessage("El teléfono debe contener exactamente 10 dígitos.")
                .Matches(@"^\d+$").WithMessage("El teléfono solo puede contener números.");

            RuleFor(x => x.Domicilio).MaximumLength(200).WithMessage("El domicilio no puede exceder los 200 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$")
                .WithMessage("El domicilio solo puede contener letras y espacios.");
        }
    }
}