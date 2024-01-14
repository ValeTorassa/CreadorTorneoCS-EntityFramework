using Entidades;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class ControladoraTorneoProfesional
    {
        private Contexto contexto;
        
        private ControladoraTorneoProfesional() 
        {
            contexto = new Contexto();
        }
        private static ControladoraTorneoProfesional instancia;
        public static ControladoraTorneoProfesional Instancia
        {
            get
            {
                if(instancia == null)
                {
                    instancia = new ControladoraTorneoProfesional();
                }
                return instancia;
            }
        }


        public string CrearTorneoProfesional(string nombre, int Total, string ubicacion, int premio)
        {
            try
            {
                if (contexto.TorneosProfesionales.Any(t => t.Nombre == nombre)) return "Ya existe un torneo con ese nombre.";

                if (Total > ObtenerJugadoresSinEquipoProfesionales().Count && Total <= 10) return "El torneo no tiene suficientes jugadores disponibles o son menos de 10.";

                if (contexto.Rangos.OrderByDescending(r => r.Escala).FirstOrDefault() == null) return "No hay rangos disponibles.";
                // Crea un torneo nuevo con los parámetros recibidos.
                TorneoProfesional torneo = new TorneoProfesional();
                torneo.Nombre = nombre;
                torneo.Ubicacion = ubicacion;
                torneo.Premio = premio;
                torneo.EquiposAleatorios(Total, ObtenerJugadoresSinEquipoProfesionales());

                // Marca a los jugadores como parte de un equipo
                foreach (var equipo in torneo.Equipos)
                {
                    foreach (var jugador in equipo.Jugadores)
                    {
                        jugador.EnEquipo = true;
                    }
                }
                contexto.TorneosProfesionales.Add(torneo);
                contexto.SaveChanges();
                return "El torneo se agregó correctamente.";
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }

        }

        public string EliminarTorneoProfesional(TorneoProfesional torneo)
        {
            try
            {
                if (!contexto.TorneosProfesionales.Any(t => t.Nombre == torneo.Nombre))
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

                contexto.TorneosProfesionales.Remove(torneo);
                contexto.SaveChanges();
                return "Torneo eliminado correctamente.";
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }

        //Metodo para obtener todos los Torneos Profesionales
        public List<TorneoProfesional> ObtenerTorneosProfesionales()
        {
            return contexto.TorneosProfesionales
            .Include(t => t.Partidas)
                .ThenInclude(p => p.Resultado)
            .Include(t => t.Equipos)
                .ThenInclude(e => e.Jugadores)
                    .ThenInclude(j => j.Rango)
            .ToList();
        }


        //Metodo para obtener todos los jugadores profesionales que no pertenecen a algun equipo
        public List<Jugador> ObtenerJugadoresSinEquipoProfesionales()
        {
            // Encontrar el rango con la mayor cantidad de puntos
            Rango rangoMaximo = contexto.Rangos.OrderByDescending(r => r.Escala).FirstOrDefault();

            if (rangoMaximo != null)
            {
                return contexto.Jugadores.Where(j => j.EnEquipo == false && j.Rango.Nombre == rangoMaximo.Nombre).ToList();
            }
            else
            {
                throw new Exception("El rango maximo no existe");
            }
        }

        public TorneoProfesional ObtenerTorneoProfesionalPorNombre(string torneoSeleccionadoNombre)
        {
            return ObtenerTorneosProfesionales().FirstOrDefault(t => t.Nombre == torneoSeleccionadoNombre);
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
    }
}
