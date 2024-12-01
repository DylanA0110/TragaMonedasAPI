using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Cuenta
    {
        public int Id_cuenta { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int Nivel_Seguridad { get; set; }
    }
}
