using System;
using System.Collections.Generic;

namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public interface IPersona
    {
        public bool CrearVinculo(IVinculo vinculo);

        public bool RomperVinculo(IVinculo vinculo);

        public void MandarMensaje(IMensaje mensaje);
    }
}
