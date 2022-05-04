using System.Collections.Generic;

namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public class Persona : IPersona
    {
        private List<IVinculo> _vinculos;
        private List<IMensaje> _mensajes;

        public Persona()
        {
            _vinculos = new List<IVinculo>();
            _mensajes = new List<IMensaje>();
        }

        public bool CrearVinculo(IVinculo vinculo)
        {
            IVinculo vinculoExistente = VinculoIgual(vinculo);
            if (vinculoExistente != null)
                return false;

            _vinculos.Add(vinculo);
            return true;
        }

        public bool RomperVinculo(IVinculo vinculo)
        {
            return _vinculos.Remove(vinculo);
        }

        public void MandarMensaje(IMensaje mensaje)
        {
            if (_mensajes.Contains(mensaje))
                return;
            _mensajes.Add(mensaje);

            foreach (IVinculo vinculo in _vinculos)
                if (vinculo.MereceMandarMensaje(mensaje))
                    vinculo.MandarMensaje(mensaje);
        }

        public IEnumerable<IMensaje> MensajesRecibidos()
        {
            foreach (IMensaje mensaje in _mensajes)
                yield return mensaje;
        }

        private IVinculo VinculoIgual(IVinculo vinculo)
        {
            return _vinculos.Find(vinculado => vinculado.MismoVinculo(vinculo));
        }
    }
}
