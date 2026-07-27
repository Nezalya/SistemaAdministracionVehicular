using FluentValidation;

namespace SAV.Core.Validators
{
    public class RegistrarClienteDtoValidator : AbstractValidator<RegistrarClienteDto>
    {
        public RegistrarClienteDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio.").
                MaximumLength(50).WithMessage("El nombre de usuario no puede exceder los 50 caracteres.");
            
            
            RuleFor(x => x.Email).NotEmpty().WithMessage("El correo es obligatorio.").
                EmailAddress().WithMessage("El formato del correo no es válido.").
                MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.");
            
            
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria.").
                MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe tener al menos una mayúscula.")
                .Matches("[0-9]").WithMessage("La contraseña debe tener al menos un número.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$")
                .WithMessage("El nombre solo puede contener letras y espacios.");

            RuleFor(x => x.Nif)
            .NotEmpty().WithMessage("El NIF es obligatorio.")
            .Length(11).WithMessage("La cédula debe contener exactamente 11 dígitos.")
            .Matches(@"^\d+$").WithMessage("La cédula solo puede contener números.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Length(10).WithMessage("El teléfono debe contener exactamente 10 dígitos.")
                .Matches(@"^\d+$").WithMessage("El teléfono solo puede contener números.");
        }
    }
}