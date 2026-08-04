using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Application.Helpers;

public static class ValidarUbicacionHelper
{
    public static void ValidarUbicacion(Automovil automovil, int concesionarioId)
    {
        bool enConcesionario = automovil.ConcesionarioUbicacionId.HasValue;
        bool enServicioOficial = automovil.ServicioOficialUbicacionId.HasValue;

        if (enConcesionario && enServicioOficial)
            throw new BusinessException(
                "El automóvil no puede estar ubicado en dos lugares a la vez (concesionario y servicio oficial).");

        if (!enConcesionario && !enServicioOficial)
            automovil.ConcesionarioUbicacionId = concesionarioId;
    }

}
