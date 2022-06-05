using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using ItIsNotOnlyMe.ComunicacionDinamica;

public partial class PersonaTest
{
    public class PersonaPrueba : Persona
    {
        public IEnumerable<IMensaje> MensajesRecibidos()
        {
            foreach (IMensaje mensaje in _mensajes)
                yield return mensaje;
        }
    }

    [Test]
    public void Test01PersonaCreaUnVinculoYReceptorRecibeElMensaje()
    {
        IPersona persona = new Persona();
        IPersona receptor = new Persona();
        IImportancia importanciaReceptor = new ImportanciaPrueba(5);

        bool pudoCrearVinculo = persona.CrearVinculo(new Vinculo(receptor, importanciaReceptor));

        Assert.IsTrue(pudoCrearVinculo);

        IImportancia importanciaMensaje = new ImportanciaPrueba(3);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        persona.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = (receptor as PersonaPrueba).MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test02PersonaCreaUnVinculoYReceptorNoSuficientementeImportanteNoRecibeElMensaje()
    {
        IPersona persona = new Persona();
        IPersona receptor = new Persona();
        IImportancia importanciaReceptor = new ImportanciaPrueba(3);

        bool pudoCrearVinculo = persona.CrearVinculo(new Vinculo(receptor, importanciaReceptor));

        Assert.IsTrue(pudoCrearVinculo);

        IImportancia importanciaMensaje = new ImportanciaPrueba(5);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        persona.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = (receptor as PersonaPrueba).MensajesRecibidos().ToList();

        Assert.IsFalse(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test03CadenaDeTresPersonasRecibenElMensaje()
    {
        IPersona personaInicio = new Persona(), personaMedio = new Persona(), personaFinal = new Persona();
        IImportancia importanciaGeneral = new ImportanciaPrueba(3);

        personaInicio.CrearVinculo(new Vinculo(personaMedio, importanciaGeneral));
        personaMedio.CrearVinculo(new Vinculo(personaFinal, importanciaGeneral));

        IImportancia importanciaMensaje = new ImportanciaPrueba(2);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        personaInicio.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = (personaFinal as PersonaPrueba).MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test04CadenaDeTresPersonaConLoopRecibenElMensaje()
    {
        IPersona personaInicio = new Persona(), personaMedio = new Persona(), personaFinal = new Persona();
        IImportancia importanciaGeneral = new ImportanciaPrueba(3);

        personaInicio.CrearVinculo(new Vinculo(personaMedio, importanciaGeneral));
        personaMedio.CrearVinculo(new Vinculo(personaFinal, importanciaGeneral));
        personaFinal.CrearVinculo(new Vinculo(personaInicio, importanciaGeneral));

        IImportancia importanciaMensaje = new ImportanciaPrueba(2);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        personaInicio.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = (personaFinal as PersonaPrueba).MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test05DosCaminosParaLlegarAUnaPersonaPeroUnoSinSuficienteImportancia()
    {
        IPersona personaInicio = new Persona();
        IPersona personaSinImportancia = new Persona(), personaConImportancia = new Persona();
        IPersona personaFinal = new Persona();

        IImportancia importanciaSuficiente = new ImportanciaPrueba(5);
        IImportancia importanciaInsuficiente = new ImportanciaPrueba(3);

        personaInicio.CrearVinculo(new Vinculo(personaSinImportancia, importanciaInsuficiente));
        personaInicio.CrearVinculo(new Vinculo(personaConImportancia, importanciaSuficiente));

        personaSinImportancia.CrearVinculo(new Vinculo(personaFinal, importanciaSuficiente));
        personaConImportancia.CrearVinculo(new Vinculo(personaFinal, importanciaSuficiente));

        IImportancia importanciaMensaje = new ImportanciaPrueba(4);
        IMensaje mensaje = new Mensaje(importanciaMensaje);
        personaInicio.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = (personaFinal as PersonaPrueba).MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test06DosCaminosPeroAlActualizarNoHayCamino()
    {
        IPersona personaInicio = new Persona();
        IPersona personaSinImportancia = new Persona(), personaConImportancia = new Persona();
        IPersona personaFinal = new Persona();

        IImportancia importanciaSuficiente = new ImportanciaPrueba(5);
        IImportancia importanciaInsuficiente = new ImportanciaPrueba(3);

        personaInicio.CrearVinculo(new Vinculo(personaSinImportancia, importanciaInsuficiente));
        personaInicio.CrearVinculo(new Vinculo(personaConImportancia, importanciaSuficiente));

        personaSinImportancia.CrearVinculo(new Vinculo(personaFinal, importanciaSuficiente));
        IVinculo vinculoSuficiente = new Vinculo(personaFinal, importanciaSuficiente);
        personaConImportancia.CrearVinculo(vinculoSuficiente);

        IImportancia importanciaMensaje = new ImportanciaPrueba(4);
        IMensaje mensaje = new Mensaje(importanciaMensaje);
        personaInicio.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = (personaFinal as PersonaPrueba).MensajesRecibidos().ToList();
        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));

        IMensaje nuevoMensaje = new Mensaje(importanciaMensaje);

        bool seRompioVinculo = personaConImportancia.RomperVinculo(vinculoSuficiente);
        Assert.IsTrue(seRompioVinculo);

        personaConImportancia.CrearVinculo(new Vinculo(personaSinImportancia, importanciaInsuficiente));
        personaInicio.MandarMensaje(nuevoMensaje);

        mensajesRecibidos = (personaFinal as PersonaPrueba).MensajesRecibidos().ToList();
        Assert.IsFalse(mensajesRecibidos.Contains(nuevoMensaje));
    }
}
