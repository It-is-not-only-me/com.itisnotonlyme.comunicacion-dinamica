namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public interface IVinculo
    {
        public bool MismoVinculo(IVinculo vinculo);

        public bool TieneVinculo(IPersona persona);

        public bool Actualizar(IImportancia importancia);

        public bool MereceMandarMensaje(IMensaje mensaje);

        public void MandarMensaje(IMensaje mensaje);
    }
}
