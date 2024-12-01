namespace PrototipoAPIBaseDeDatos.SharedModels
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Cuenta { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
