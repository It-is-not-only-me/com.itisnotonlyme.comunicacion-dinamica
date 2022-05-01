namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public class Mensaje : IMensaje
    {
        private IImportancia _importancia;

        public Mensaje(IImportancia importancia)
        {
            _importancia = importancia;
        }

        public bool MereceMandarMensaje(IImportancia importancia)
        {
            return importancia.EsMayorIgual(_importancia);
        }
    }
}
