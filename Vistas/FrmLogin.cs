using PrototipoAPIBaseDeDatos.SharedModels;
using SharedModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistas.Services;

namespace WinFormsApp1
{
    public partial class FrmLogin : Form
    {
        private UsuarioService _usuarioService;

        public FrmLogin()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario
            {
                Cuenta = txtUsuario.Text, // El texto que el usuario introduce en el campo Cuenta
                Contraseña = txtContraseña.Text // El texto que el usuario introduce en el campo Contraseña
            };

            var usuarioAutenticado = await _usuarioService.LoginAsync(usuario);

            if (usuarioAutenticado != null)
            {
                MessageBox.Show("Bienvenido, " + usuarioAutenticado.Cuenta);
                this.Hide();
                FrmPrincipal frm = new FrmPrincipal();
                frm.Show();
            
            }
            else
            {
                // Mostrar mensaje de error si las credenciales son incorrectas
                MessageBox.Show("Credenciales inválidas. Intenta nuevamente.");
            }
        }

        
    }
}
