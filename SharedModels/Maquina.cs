using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Maquina
    {
        public int ID_Maquina { get; set; }
        public string Nombre_Maquina { get; set; }
        public int ID_Tipo_Maquina { get; set; }
        public string Estado { get; set; } 
        public DateTime? Fecha_Instalacion { get; set; }
        public int ID_Sala { get; set; }

        
    }
}
