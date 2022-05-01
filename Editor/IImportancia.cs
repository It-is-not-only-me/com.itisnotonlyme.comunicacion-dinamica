namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public interface IImportancia
    {
        public int ValorImportancia { get; }

        public bool EsMayorIgual(IImportancia importancia);
    }
}
