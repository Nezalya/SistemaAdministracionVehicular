using FluentValidation;

namespace SAV.Core.Validators
{
    public class CrearVentaDtoValidator : AbstractValidator<CrearVentaDto>
    {
        public CrearVentaDtoValidator()
        {
            RuleFor(x => x.AutomovilId).GreaterThan(0).WithMessage("Debe indicar un automóvil válido.");
            RuleFor(x => x.ClienteId).GreaterThan(0).WithMessage("Debe indicar un cliente válido.");

            RuleFor(x => x.FormaPago).IsInEnum().WithMessage("La forma de pago no es válida.");
            RuleFor(x => x.Origen).IsInEnum().WithMessage("El origen de la venta no es válido.");

            // RN29 — fecha de entrega y matrícula obligatorias
            RuleFor(x => x.FechaEntrega)
                .NotNull().WithMessage("La fecha de entrega es obligatoria.");
            RuleFor(x => x.Matricula)
                .NotEmpty().WithMessage("La matrícula es obligatoria.")
                .MaximumLength(10).WithMessage("La matrícula no puede exceder los 10 caracteres.");

            // RN26 — XOR: exactamente un responsable (formato, sin BD)
            RuleFor(x => x)
                .Must(v => v.VendedorId.HasValue ^ v.ServicioOficialId.HasValue)
                .WithMessage("La venta debe tener exactamente un responsable: un vendedor o un servicio oficial.");

            RuleFor(x => x.Financiera)
            .NotEmpty().WithMessage("No puede estar vacía la financiera.")
            .When(x => x.FormaPago == FormaPago.Financiera)
             .WithMessage("Debe indicar la financiera cuando el pago es financiado.")
             .MaximumLength(100).WithMessage("La financiera no puede exceder los 100 caracteres.");

            // No permitir extras repetidos en la misma venta
            RuleFor(x => x.CaracteristicaIdsExtras)
                .Must(lista => lista.Distinct().Count() == lista.Count)
                .WithMessage("No se pueden repetir extras en la misma venta.");
        }
    }
}