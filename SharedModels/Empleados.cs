using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Empleado
    {
        public int ID_Empleados { get; set; }  // Clave primaria
        public string Nombres { get; set; }  // Nombre del empleado
        public string Apellidos { get; set; }  // Apellidos del empleado
        public int id_Puesto { get; set; }  // ID del puesto
        public string Turno { get; set; }  // Turno del empleado
        public int ID_Sala { get; set; }  // ID de la sala
       
    }
}
