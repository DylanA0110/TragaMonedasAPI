using SharedModels.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Premio
    {
        public int ID_Premio { get; set; }
        public int ID_Cliente { get; set; }
        public int ID_Maquina { get; set; }
        public int Monto_Total { get; set; }
        public DateTime FechaGanadoElPremio { get; set; }
        public DateTime? FechaEntregadoElPremio { get; set; }

       
    }
}
