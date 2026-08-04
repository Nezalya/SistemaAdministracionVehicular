using FluentValidation;

namespace SAV.Application.Validators
{
    public class GenerarAutomovilesLoteDtoValidator : AbstractValidator<GenerarAutomovilesLoteDto>
    {
        public GenerarAutomovilesLoteDtoValidator()
        {
            RuleFor(x => x.ModeloId).GreaterThan(0).WithMessage("Debe indicar un modelo válido.");
            
            RuleFor(x => x.CantidadPorColor)
                .InclusiveBetween(1, 100).WithMessage("La cantidad por color debe estar entre 1 y 100.");
            
            RuleFor(x => x.Colores)
                .NotEmpty().WithMessage("Debe especificar al menos un color.")
                .WithMessage("Debe especificar al menos un color.");
        }
    }
}