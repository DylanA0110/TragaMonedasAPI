using SharedModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class PuestoService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/puesto"; // URL base de la API de puestos

        public PuestoService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todos los puestos
        public async Task<List<Puesto>> ObtenerPuestos()
        {
            return await _apiClient.ObtenerTodos<Puesto>(_baseUrl);
        }

        // Obtener un puesto por ID
        public async Task<Puesto> ObtenerPuestoPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Puesto>(_baseUrl, id);
        }

        // Crear un nuevo puesto
        public async Task<bool> CrearPuesto(Puesto puesto)
        {
            return await _apiClient.Crear(_baseUrl, puesto);
        }

        // Actualizar un puesto
        public async Task<bool> ActualizarPuesto(int id, Puesto puesto)
        {
            return await _apiClient.Actualizar(_baseUrl, id, puesto);
        }

        // Eliminar un puesto por ID
        public async Task<bool> EliminarPuesto(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }
}
