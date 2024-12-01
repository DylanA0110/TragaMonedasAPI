using SharedModels;
using SharedModels.SharedModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class PremioService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/premio"; // URL base de la API de premios

        public PremioService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todos los premios
        public async Task<List<Premio>> ObtenerTodosLosPremios()
        {
            return await _apiClient.ObtenerTodos<Premio>(_baseUrl);
        }

        // Obtener un premio por ID
        public async Task<Premio> ObtenerPremioPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Premio>(_baseUrl, id);
        }

        // Crear un nuevo premio
        public async Task<bool> CrearPremio(Premio premio)
        {
            return await _apiClient.Crear(_baseUrl, premio);
        }

        // Actualizar un premio existente
        public async Task<bool> ActualizarPremio(int id, Premio premio)
        {
            return await _apiClient.Actualizar(_baseUrl, id, premio);
        }

        // Eliminar un premio por ID
        public async Task<bool> EliminarPremio(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }
}
