using Entidades;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class ControladoraJugador
    {
        private ControladoraJugador() { }
        private static ControladoraJugador instancia;
        private Contexto contexto = new Contexto();
        public static ControladoraJugador Instancia
        {
            get
            {
                if (instancia == null) instancia = new ControladoraJugador();
                return instancia;
            }
        }
        //Metodo para crear un nuevo Jugador
        public string CrearJugador(string nombre, string nombreRango)
        {
            try
            {
                if (ObtenerJugadores().Any(j => j.Nombre == nombre)) return "Ya existe un jugador con ese nombre.";

                Rango rango = contexto.Rangos.FirstOrDefault(x=> x.Nombre == nombreRango);
                if (rango == null) return "El rango especificado no existe.";

                Jugador nuevoJugador = new Jugador
                {
                    Nombre = nombre,

                    Rango = rango
                };

                contexto.Jugadores.Add(nuevoJugador);
                contexto.SaveChanges();
                return  "El jugador se agregó correctamente.";

            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }
        public string EliminarJugador(Jugador jugador)
        {
            try
            {
                if (!ObtenerJugadores().Any(j => j.Nombre == jugador.Nombre))
                {
                    return "No se encontró el jugador a eliminar.";
                }

                contexto.Jugadores.Remove(jugador);
                contexto.SaveChanges();
                return "Jugador eliminado correctamente.";
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }

        public List<Jugador> ObtenerJugadores()
        {
            return contexto.Jugadores.Include(X=>X.Rango).ToList();
        }

        public List<Jugador> ObtenerJugadoresSinEquipo()
        {
            return ObtenerJugadores().Where(j => j.EnEquipo == false).ToList();
        }

    }
}
