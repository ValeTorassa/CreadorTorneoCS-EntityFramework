using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EstadisticasEquipo
    {
        [Key] public int EstadisticasEquipoId { get; set; }
        public int Victorias { get => _victorias; set { _victorias = value; _perdidas = CalcularPerdidas(); } }
        public int Perdidas { get => _perdidas; }
        public int RondasGanadas { get => _rondasGanadas; set { _rondasGanadas = value; _rondasPerdidas = CalcularRondasPerdidas(); } }
        public int RondasPerdidas { get => _rondasPerdidas; }
        public int Kills { get => _kills; set => _kills = value; }
        public int TotalPartidas { get => _totalPartidas; set { _totalPartidas = value; _perdidas = CalcularPerdidas(); } }
        public int TotalRondas { get => _totalRondas; set { _totalRondas = value; _rondasPerdidas = CalcularRondasPerdidas(); } }

        // Método privado para calcular el número de rondas perdidas.
        private int CalcularRondasPerdidas()
        {
            return _totalRondas - _rondasGanadas;
        }

        // Método privado para calcular el número de derrotas.
        private int CalcularPerdidas()
        {
            return _totalPartidas - _victorias;
        }

        // Método público para calcular el porcentaje de victorias.
        public int PorcentajeVictorias()
        {
            return Victorias * 100 / TotalPartidas;
        }

        public override string ToString()
        {
            return "Calculadas";
        }

        // Variables privadas utilizadas para almacenar los datos internamente en la clase.
        private int _victorias;
        private int _perdidas;
        private int _totalPartidas;
        private int _rondasGanadas;
        private int _rondasPerdidas;
        private int _totalRondas;
        private int _kills;


    }
}
