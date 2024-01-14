using Entidades;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System.Collections.ObjectModel;

namespace Controladora
{
    public class ControladoraTorneoCasual
    {
        private ControladoraTorneoCasual()
        {
            contexto = new Contexto();
        }

        private Contexto contexto;

        private static ControladoraTorneoCasual instancia;
        public static ControladoraTorneoCasual Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ControladoraTorneoCasual();
                }
                return instancia;
            }
        }

        //Metodo para crear un Torneo Casual
        public string CrearTorneoCasual(string nombre, int Total)
        {
            try
            {
                if (contexto.TorneosCasuales.Any(t => t.Nombre == nombre)) return "Ya existe un torneo con ese nombre.";

                if (Total > ObtenerJugadoresSinEquipo().Count && Total <= 10) return "El torneo no tiene suficientes jugadores disponibles o son menos de 10.";

                Torneo torneo = new Torneo();
                torneo.Nombre = nombre;
                torneo.EquiposAleatorios(Total, ObtenerJugadoresSinEquipo());

                foreach (var equipo in torneo.Equipos)
                {
                    foreach (var jugador in equipo.Jugadores)
                    {
                        jugador.EnEquipo = true;
                    }
                }

                contexto.TorneosCasuales.Add(torneo);
                contexto.SaveChanges();

                return "El torneo se agregó correctamente.";

            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }

        }


        // Método para Eliminar un Torneo Casual
        public string EliminarTorneoCasual(Torneo torneo)
        {
            try
            {
                if (!contexto.TorneosCasuales.Any(t => t.Nombre == torneo.Nombre))
                {
                    return "No se encontró el torneo a eliminar.";
                }

                foreach (Equipo equipo in torneo.Equipos)
                {
                    foreach (Jugador jugador in equipo.Jugadores)
                    {
                        jugador.EnEquipo = false;
                    }
                }
                contexto.TorneosCasuales.Remove(torneo);
                contexto.SaveChanges();
                return "Torneo eliminado correctamente.";
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }


        //Metodo para obtener todos los Torneos Casuales
        public List<Torneo> ObtenerTorneosCasuales()
        {
            return contexto.TorneosCasuales
            .Include(t => t.Partidas)
                .ThenInclude(p=> p.Resultado)
            .Include(t => t.Equipos)
                .ThenInclude(e => e.Jugadores)
                    .ThenInclude(j => j.Rango)
            .Where(t => !(t is TorneoProfesional))
            .ToList();
        }

        // Método para registrar o modificar un resultado de partida en función del estado de la partida.
        public string RegistrarModificarResultado(Partida partida, ResultadoPartida resultado)
        {
            try
            {
                if (!partida.PartidaJugada)
                {
                    partida.RegistrarResultados(resultado);
                    contexto.Update(partida);
                    contexto.SaveChanges();
                    return "El resultado se ha registrado";
                }
                else
                {
                    partida.ModificarResultados(resultado);
                    contexto.Update(partida);
                    contexto.SaveChanges();
                    return "El resultado se ha modificado";
                }
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }

        //Metodo para obtener todos los jugadores que no pertenecen a algun equipo
        public List<Jugador> ObtenerJugadoresSinEquipo()
        {
            return contexto.Jugadores.Include(j => j.Rango).Where(j => j.EnEquipo == false).ToList();
        }

        public Torneo ObtenerTorneoPorNombre(string torneoSeleccionadoNombre)
        {
            return ObtenerTorneosCasuales().FirstOrDefault(t => t.Nombre == torneoSeleccionadoNombre);
        }
    }
}
