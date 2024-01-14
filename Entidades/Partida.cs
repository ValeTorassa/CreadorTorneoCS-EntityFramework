using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Partida
    {
        // Propiedades públicas para obtener información de la partida.
        [Key] public int PartidaId { get; set; }
        public string NombreLocal { get ; set; }
        public string NombreVisitante { get ; set; }
        public bool PartidaJugada { get; set; }
        public int ResultadoPartidaId { get; set; }
        public ResultadoPartida Resultado { get => _resultado; }


        public Partida() 
        {
            _resultado = new ResultadoPartida();
        }


        // Método para registrar los resultados de la partida.
        public void RegistrarResultados(ResultadoPartida resultado)
        {
            if (PartidaJugada == false)
            {
                _resultado = resultado;
                PartidaJugada = true;
            }
        }

        // Método para modificar los resultados de la partida.
        public void ModificarResultados(ResultadoPartida resultado)
        {
            if (PartidaJugada == true)
            {
                _resultado = resultado;
            }
        }

        private ResultadoPartida _resultado;

    }
}
