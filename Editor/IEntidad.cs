
using System;
using System.Collections.Generic;

namespace ItIsNotOnlyMe.ComunicacionDinamica
{
    public interface IPersona
    {
        public void AgregarConeccion(IPersona persona, IRelacion relacion);

        public void SacarConeccion(IPersona persona);

        public void RecibirMensaje(IMensaje mensaje);

        public IPrioridad EvaluarMensaje(IMensaje mensaje);

        public void DifundirMensajes();
    }

    public class Persona : IPersona
    {
        private List<Tuple<IPersona, IRelacion>> _conecciones;
        private HashSet<IMensaje> _mensajes;

        public Persona()
        {
            _conecciones = new List<Tuple<IPersona, IRelacion>>();
            _mensajes = new HashSet<IMensaje>();
        }

        public void AgregarConeccion(IPersona persona, IRelacion relacion)
        {
            _conecciones.Add(new Tuple<IPersona, IRelacion>(persona, relacion));
        }

        public void SacarConeccion(IPersona persona)
        {
            Tuple<IPersona, IRelacion> personaRelacion = _conecciones.Find(par => par.Item1.Equals(persona));
            _conecciones.Remove(personaRelacion);
        }

        public void RecibirMensaje(IMensaje mensaje)
        {
            _mensajes.Add(mensaje);
        }

        public IPrioridad EvaluarMensaje(IMensaje mensaje)
        {
            throw new System.NotImplementedException();
        }

        public void DifundirMensajes()
        {
            List<IMensaje> mensajesAEliminar = new List<IMensaje>();
            foreach (IMensaje mensaje in _mensajes)
            {
                // si tenes conecciones, entonces puede ser que difundas
                bool seDifundioCompletamente = _conecciones.Count > 0; 

                foreach (Tuple<IPersona, IRelacion> personaRelacion in _conecciones)
                {
                    bool seDifunde = mensaje.PermiteDifundir(personaRelacion.Item2);
                    seDifundioCompletamente &= seDifunde;

                    if (seDifunde)
                        personaRelacion.Item1.RecibirMensaje(mensaje);
                }
                if (seDifundioCompletamente)
                    mensajesAEliminar.Add(mensaje);
            }

            mensajesAEliminar.ForEach(mensaje => _mensajes.Remove(mensaje));
        }
    }

    public interface IRelacion
    {

    }

    public interface IPrioridad
    {
        public bool PermiteDifundir(IRelacion relacion);
    }

    public interface IMensaje
    {
        public bool PermiteDifundir(IRelacion relacion);
    }
}
