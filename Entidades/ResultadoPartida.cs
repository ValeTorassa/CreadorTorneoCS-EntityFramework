using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ResultadoPartida
    {
        // Properties
        [Key] public int ResultadoPartidaId { get; set; }
        public bool GanadorLocal { get; set; }
        public bool GanadorVisitante { get; set; }
        public int RondasLocal { get; set; }
        public int RondasVisitante { get; set; }
        public int MuertesLocal { get; set; }
        public int MuertesVisitante { get; set; }
        public int Duracion { get; set; }
        public string Mapa { get; set; }
        public int PartidaId { get; set; }
        public Partida Partida { get; set; }

        // Parameterless constructor
        public ResultadoPartida()
        {
        }

        // Constructor with parameters
        public ResultadoPartida(bool ganadorLocal, int rondasLocal, int muertesLocal, bool ganadorVisitante, int rondasVisitante, int muertesVisitante, int duracion, string mapa, Partida partida)
        {
            GanadorLocal = ganadorLocal;
            RondasLocal = rondasLocal;
            MuertesLocal = muertesLocal;
            GanadorVisitante = ganadorVisitante;
            RondasVisitante = rondasVisitante;
            MuertesVisitante = muertesVisitante;
            Duracion = duracion;
            Mapa = mapa;
            Partida = partida;
        }

        public override string ToString()
        {
            if (GanadorLocal)
            {
                return "Equipo Local";
            }
            else if (GanadorVisitante)
            {
                return "Equipo Visitante";
            }
            else
            {
                return "Empate";
            }
        }
    }
}
