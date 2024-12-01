using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class SalaService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/salas"; // La URL de la API de salas

        public SalaService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todas las salas
        public async Task<List<Sala>> ObtenerSalas()
        {
            return await _apiClient.ObtenerTodos<Sala>(_baseUrl);
        }

        // Obtener una sala por ID
        public async Task<Sala> ObtenerSalaPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Sala>(_baseUrl, id);
        }

        // Crear una nueva sala
        public async Task<bool> CrearSala(Sala sala)
        {
            return await _apiClient.Crear(_baseUrl, sala);
        }

        // Actualizar una sala
        public async Task<bool> ActualizarSala(int id, Sala sala)
        {
            return await _apiClient.Actualizar(_baseUrl, id, sala);
        }

        // Eliminar una sala por ID
        public async Task<bool> EliminarSala(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }
}
