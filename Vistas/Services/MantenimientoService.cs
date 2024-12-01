using SharedModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class MantenimientoService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/mantenimiento"; // URL base de la API de mantenimiento

        public MantenimientoService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todos los mantenimientos
        public async Task<List<Mantenimiento>> ObtenerMantenimientos()
        {
            return await _apiClient.ObtenerTodos<Mantenimiento>(_baseUrl);
        }

        // Obtener un mantenimiento por ID
        public async Task<Mantenimiento> ObtenerMantenimientoPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Mantenimiento>(_baseUrl, id);
        }

        // Crear un nuevo mantenimiento
        public async Task<bool> CrearMantenimiento(Mantenimiento mantenimiento)
        {
            return await _apiClient.Crear(_baseUrl, mantenimiento);
        }

        // Actualizar un mantenimiento
        public async Task<bool> ActualizarMantenimiento(int id, Mantenimiento mantenimiento)
        {
            return await _apiClient.Actualizar(_baseUrl, id, mantenimiento);
        }

        // Eliminar un mantenimiento por ID
        public async Task<bool> EliminarMantenimiento(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }
}
