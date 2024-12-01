using SharedModels;
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
    public partial class FrmAñadirEmpleado : Form
    {
        //Prueba
        private EmpleadoService _empleadoService;
        public FrmAñadirEmpleado()
        {
            InitializeComponent();
            _empleadoService = new EmpleadoService();
        }

        private async void btnAggE_Click(object sender, EventArgs e)
        {
            var empleado= new Empleado
            {
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                id_Puesto = int.Parse(txtPuesto.Text),
                ID_Sala = int.Parse(txtSala.Text),
                Turno = txtTurno.Text,
            };

            var result = await _empleadoService.CrearEmpleado(empleado);

            // Mostrar un mensaje según el resultado
            if (result)
            {
                MessageBox.Show("Empleado agregado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            // Limpiar los TextBox
            txtNombres.Clear();
            txtApellidos.Clear();
            txtPuesto.Clear();
            txtSala.Clear();
            txtTurno.Clear();
        }

    }
}
