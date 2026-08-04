using FluentValidation;

namespace SAV.Application.Validators
{
    public class RegistrarAdministradorDtoValidator : AbstractValidator<RegistrarAdministradorDto>
    {
        public RegistrarAdministradorDtoValidator()
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
        }
    }
}