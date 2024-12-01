using Microsoft.Extensions.Configuration;
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
    public partial class Clientes : Form
    {
        private ClienteService _clienteService;
        private EmpleadoService _empleadoService;
        public Clientes()
        {
            InitializeComponent();
           _clienteService = new ClienteService();
            _empleadoService = new EmpleadoService();
        }

        private void btnAñadirC_Click(object sender, EventArgs e)
        {
            FrmAñadirCliente frmAñadir = new FrmAñadirCliente();
            frmAñadir.ShowDialog();
        }

        private async Task MostrarClientes()
        {
            // Llamar a la API para obtener los clientes
            List<Cliente> clientes = await _clienteService.ObtenerClientes();

            if (clientes != null)
            {
                dtgClientes.DataSource = clientes; // Mostrar los clientes en el DataGridView
            }
            else
            {
                MessageBox.Show("Error al obtener clientes.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void Clientes_Load(object sender, EventArgs e)
        {
            await MostrarClientes();

            await MostrarEmpleados();
        }

        private async Task MostrarEmpleados()
        {
            // Llamar a la API para obtener los clientes
            List<Empleado> emps = await _empleadoService.ObtenerEmpleados();

            if (emps!= null)
            {
                dgvEmpleado.DataSource = emps; // Mostrar los clientes en el DataGridView
            }
            else
            {
                MessageBox.Show("Error al obtener Empleados.");
            }
        }
    }
}
