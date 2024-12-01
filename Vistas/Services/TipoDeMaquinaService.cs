using SharedModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class TipoDeMaquinaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7052/api/tipodemaquinas"; // Update with your actual API URL

        public TipoDeMaquinaService()
        {
            _httpClient = new HttpClient();
        }

        // Obtener todos los tipos de máquinas
        public async Task<List<TipoDeMaquina>> ObtenerTiposDeMaquinas()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TipoDeMaquina>>();
            }
            return null; // Or handle it with custom error handling
        }

        // Obtener un tipo de máquina por ID
        public async Task<TipoDeMaquina> ObtenerTipoDeMaquinaPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TipoDeMaquina>();
            }
            return null; // Or handle not found error
        }

        // Crear un nuevo tipo de máquina
        public async Task<bool> CrearTipoDeMaquina(TipoDeMaquina tipo)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, tipo);
            return response.IsSuccessStatusCode;
        }

        // Actualizar un tipo de máquina
        public async Task<bool> ActualizarTipoDeMaquina(int id, TipoDeMaquina tipo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", tipo);
            return response.IsSuccessStatusCode;
        }

        // Eliminar un tipo de máquina por ID
        public async Task<bool> EliminarTipoDeMaquina(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
