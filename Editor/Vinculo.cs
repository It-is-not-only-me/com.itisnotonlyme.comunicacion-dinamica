namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public class Vinculo : IVinculo
    {
        protected IPersona _persona;
        protected IImportancia _importancia;

        public Vinculo(IPersona persona, IImportancia importancia)
        {
            _persona = persona;
            _importancia = importancia;
        }

        public bool Actualizar(IImportancia importancia)
        {
            _importancia = importancia;
            return true;
        }

        public void MandarMensaje(IMensaje mensaje)
        {
            _persona.MandarMensaje(mensaje);
        }

        public bool MereceMandarMensaje(IMensaje mensaje)
        {
            return mensaje.MereceMandarMensaje(_importancia);
        }

        public bool MismoVinculo(IVinculo vinculo)
        {
            return vinculo.TieneVinculo(_persona);
        }

        public bool TieneVinculo(IPersona persona)
        {
            return _persona == persona;
        }
    }
}
