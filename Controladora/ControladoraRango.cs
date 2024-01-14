using Entidades;
using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class ControladoraRango
    {
        private ControladoraRango() { }
        private static ControladoraRango instancia;
        private Contexto contexto = new Contexto();
        public static ControladoraRango Instancia
        {
            get
            {
                if (instancia == null)
                {
                   instancia = new ControladoraRango();
                }
                return instancia;
            }
        }

        //Metodo para crear un nuevo Rango
        public string CrearRango(string nombre, int puntos)
        {
            try
            {
                if (ObtenerRangos().Any(r => r.Nombre == nombre)) return "Ya existe un Rango con ese Nombre";

                Rango rango = new Rango(nombre, puntos);
                contexto.Add(rango);
                contexto.SaveChanges();
                return  "El rango se ha agregado correctamente";
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }
        // Método para eliminar un Rango
        public string EliminarRango(Rango rango)
        {
            try
            {
                if (!ObtenerRangos().Any(j => j.Nombre == rango.Nombre))
                {
                    return "No se encontró el rango a eliminar.";
                }
                contexto.Rangos.Remove(rango);
                contexto.SaveChanges();
                return "Rango eliminado correctamente.";
            }
            catch (Exception e)
            {
                return "Ha ocurrido una excepcion: " + e.Message;
            }
        }

        //Metodo para obtener todos los Rangos
        public List<Rango> ObtenerRangos()
        {
            return contexto.Rangos.ToList();
        }
      
 
    }
}
