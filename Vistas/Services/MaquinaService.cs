using SharedModels;
using SharedModels.SharedModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class MaquinaService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/maquina"; // URL base de la API de máquinas

        public MaquinaService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todas las máquinas
        public async Task<List<Maquina>> ObtenerMaquinas()
        {
            return await _apiClient.ObtenerTodos<Maquina>(_baseUrl);
        }

        // Obtener una máquina por ID
        public async Task<Maquina> ObtenerMaquinaPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Maquina>(_baseUrl, id);
        }

        // Crear una nueva máquina
        public async Task<bool> CrearMaquina(Maquina maquina)
        {
            return await _apiClient.Crear(_baseUrl, maquina);
        }

        // Actualizar una máquina
        public async Task<bool> ActualizarMaquina(int id, Maquina maquina)
        {
            return await _apiClient.Actualizar(_baseUrl, id, maquina);
        }

        // Eliminar una máquina por ID
        public async Task<bool> EliminarMaquina(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }
}
