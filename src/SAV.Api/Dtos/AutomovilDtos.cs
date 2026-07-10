using System;
using SAV.Core.Enums;

namespace SAV.Api.Dtos
{
    public record AutomovilDto(int Id, int ModeloId, string NumBastidor, string? Color, int? AnoFabricacion, EstadoAutomovil Estado);

    public record CreateAutomovilDto(int ModeloId, string NumBastidor, string? Color, int? AnoFabricacion, EstadoAutomovil Estado, int ConcesionarioPropietarioId);
}
