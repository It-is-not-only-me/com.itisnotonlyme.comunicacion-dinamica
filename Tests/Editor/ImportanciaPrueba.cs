using ItIsNotOnlyMe.ComunicacionDinamica;

public class ImportanciaPrueba : IImportancia
{
    public int ValorImportancia => _importancia;
    private int _importancia;

    public ImportanciaPrueba(int importancia)
    {
        _importancia = importancia;
    }

    public bool EsMayorIgual(IImportancia importancia)
    {
        ImportanciaPrueba importanciaPrueba = importancia as ImportanciaPrueba;
        return _importancia >= importanciaPrueba.ValorImportancia;
    }
}