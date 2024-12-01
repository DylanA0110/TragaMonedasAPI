using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Mantenimiento
    {
        public int ID_Mantenimiento { get; set; }
        public int ID_Maquina { get; set; }
        public int ID_Empleado { get; set; }
        public int ID_Tipo_Mantenimiento { get; set; }
        public DateTime Fecha_Mantenimiento { get; set; }
        public string Descripcion { get; set; }

     
       
    }
}
