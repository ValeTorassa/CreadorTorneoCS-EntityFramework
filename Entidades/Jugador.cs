using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Jugador
    {
        //Propiedades publicas de la clase Jugador    
       [Key] public int JugadorId { get; set; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int RangoId { get; set; }
        public Rango Rango { get => _rango; set => _rango = value; }

        public bool EnEquipo { get => _enEquipo; set => _enEquipo = value; }

        //Constructor de la clase jugador
        public Jugador()
        {
            //Inicialmente el jugador no estara en ningun equipo
            EnEquipo = false;
        }
        // Método para obtener una representación legible del jugador
        public override string ToString()
        {
            return Nombre;// Retorna el nombre del jugador
        }

        //Atributos privados de la clase Jugador
        private string _nombre;
        private Rango _rango;
        private bool _enEquipo;
    }
}
