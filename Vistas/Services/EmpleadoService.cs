using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class EmpleadoService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/empleado"; // La URL de la API de empleados

        public EmpleadoService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todos los empleados
        public async Task<List<Empleado>> ObtenerEmpleados()
        {
            return await _apiClient.ObtenerTodos<Empleado>(_baseUrl);
        }

        // Obtener un empleado por ID
        public async Task<Empleado> ObtenerEmpleadoPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Empleado>(_baseUrl, id);
        }

        // Crear un nuevo empleado
        public async Task<bool> CrearEmpleado(Empleado empleado)
        {
            return await _apiClient.Crear(_baseUrl, empleado);
        }

        // Actualizar un empleado
        public async Task<bool> ActualizarEmpleado(int id, Empleado empleado)
        {
            return await _apiClient.Actualizar(_baseUrl, id, empleado);
        }

        // Eliminar un empleado por ID
        public async Task<bool> EliminarEmpleado(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }

}
