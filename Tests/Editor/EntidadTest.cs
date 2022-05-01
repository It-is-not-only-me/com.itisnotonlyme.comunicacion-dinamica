using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using ItIsNotOnlyMe.ComunicacionDinamica;

public class PersonaTest
{
    private class ImportanciaPreuba : IImportancia
    {
        public int ValorImportancia => _importancia;
        private int _importancia;

        public ImportanciaPreuba(int importancia)
        {
            _importancia = importancia;
        }


        public bool EsMayorIgual(IImportancia importancia)
        {
            return _importancia >= importancia.ValorImportancia;
        }
    }

    [Test]
    public void Test01PersonaCreaUnVinculoYReceptorRecibeElMensaje()
    {
        IPersona persona = new Persona();
        IPersona receptor = new Persona();
        IImportancia importanciaReceptor = new ImportanciaPreuba(5);

        bool pudoCrearVinculo = persona.CrearVinculo(new Vinculo(receptor, importanciaReceptor));

        Assert.IsTrue(pudoCrearVinculo);

        IImportancia importanciaMensaje = new ImportanciaPreuba(3);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        persona.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = receptor.MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test02PersonaCreaUnVinculoYReceptorNoSuficientementeImportanteNoRecibeElMensaje()
    {
        IPersona persona = new Persona();
        IPersona receptor = new Persona();
        IImportancia importanciaReceptor = new ImportanciaPreuba(3);

        bool pudoCrearVinculo = persona.CrearVinculo(new Vinculo(receptor, importanciaReceptor));

        Assert.IsTrue(pudoCrearVinculo);

        IImportancia importanciaMensaje = new ImportanciaPreuba(5);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        persona.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = receptor.MensajesRecibidos().ToList();

        Assert.IsFalse(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test03CadenaDeTresPersonasRecibenElMensaje()
    {
        IPersona personaInicio = new Persona(), personaMedio = new Persona(), personaFinal = new Persona();
        IImportancia importanciaGeneral = new ImportanciaPreuba(3);

        personaInicio.CrearVinculo(new Vinculo(personaMedio, importanciaGeneral));
        personaMedio.CrearVinculo(new Vinculo(personaFinal, importanciaGeneral));

        IImportancia importanciaMensaje = new ImportanciaPreuba(2);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        personaInicio.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = personaFinal.MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }

    [Test]
    public void Test04CadenaDeTresPersonaConLoopRecibenElMensaje()
    {
        IPersona personaInicio = new Persona(), personaMedio = new Persona(), personaFinal = new Persona();
        IImportancia importanciaGeneral = new ImportanciaPreuba(3);

        personaInicio.CrearVinculo(new Vinculo(personaMedio, importanciaGeneral));
        personaMedio.CrearVinculo(new Vinculo(personaFinal, importanciaGeneral));
        personaFinal.CrearVinculo(new Vinculo(personaInicio, importanciaGeneral));

        IImportancia importanciaMensaje = new ImportanciaPreuba(2);
        IMensaje mensaje = new Mensaje(importanciaMensaje);

        personaInicio.MandarMensaje(mensaje);

        List<IMensaje> mensajesRecibidos = personaFinal.MensajesRecibidos().ToList();

        Assert.IsTrue(mensajesRecibidos.Contains(mensaje));
    }
}
