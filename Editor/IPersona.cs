using System;
using System.Collections.Generic;

namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public interface IPersona
    {
        public bool CrearVinculo(IVinculo nuevoVinculo);

        public bool ActualizarVinculo(IPersona persona, IImportancia importancia);

        public void MandarMensaje(IMensaje mensaje);

        public IEnumerable<IMensaje> MensajesRecibidos();
    }
}
