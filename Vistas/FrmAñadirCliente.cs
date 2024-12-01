using Microsoft.VisualBasic.Logging;
using SharedModels;
using SharedModels.SharedModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistas.Services;


namespace WinFormsApp1
{
    public partial class FrmAñadirCliente : Form
    {
        private ClienteService _clienteService;

        public FrmAñadirCliente()
        {
            InitializeComponent();
            _clienteService = new ClienteService();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnAñadirC_Click(object sender, EventArgs e)
        {
          
            var cliente = new Cliente
            {
                Nombres = txtNom.Text,
                Apellidos = txtApe.Text,
                Direccion = txtDir.Text,
                No_Telefono = txtTel.Text,
                Sexo = txtSex.Text,
                Profesion = txtProf.Text,
                No_Cedula = txtCed.Text,
            };

                // Llamar a la API para agregar el cliente
                var result = await _clienteService.CrearCliente(cliente);

                // Mostrar un mensaje según el resultado
                if (result)
                {
                    MessageBox.Show("Cliente agregado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void LimpiarFormulario()
        {
            txtNom.Clear();
            txtApe.Clear();
            txtDir.Clear();
            txtTel.Clear();
            txtSex.Clear();
            txtProf.Clear();
            txtCed.Clear();
        }
    }
}
