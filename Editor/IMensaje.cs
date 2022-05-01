namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public interface IMensaje
    {
        public bool MereceMandarMensaje(IImportancia importancia);
    }
}
