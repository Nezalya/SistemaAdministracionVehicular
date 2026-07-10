using System;
using System.Collections.Generic;
using SAV.Core.Enums;

namespace SAV.Api.Dtos
{
    public record VentaDto(int Id, int AutomovilId, int ClienteId, int? VendedorId, decimal PrecioCobrado, FormaPago FormaPago, DateTime FechaVenta, OrigenVenta Origen);

    public record CreateVentaDto(int AutomovilId, int ClienteId, int? VendedorId, decimal PrecioCobrado, FormaPago FormaPago, DateTime FechaVenta, OrigenVenta Origen, List<int> CaracteristicaIdsExtras);
}
