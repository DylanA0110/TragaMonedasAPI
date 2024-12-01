using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace Vistas.Services
{
    public class TipoDeMantenimientoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7052/api/tipodemantenimiento"; // La URL de la API de tipos de mantenimiento

        public TipoDeMantenimientoService()
        {
            _httpClient = new HttpClient();
        }

        // Obtener todos los tipos de mantenimiento
        public async Task<List<TipoDeMantenimiento>> ObtenerTiposDeMantenimiento()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TipoDeMantenimiento>>();
            }
            return null; // Or handle it with custom error handling
        }

        // Obtener un tipo de mantenimiento por ID
        public async Task<TipoDeMantenimiento> ObtenerTipoDeMantenimientoPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TipoDeMantenimiento>();
            }
            return null; // Or handle not found error
        }

        // Crear un nuevo tipo de mantenimiento
        public async Task<bool> CrearTipoDeMantenimiento(TipoDeMantenimiento tipo)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, tipo);
            return response.IsSuccessStatusCode;
        }

        // Actualizar un tipo de mantenimiento
        public async Task<bool> ActualizarTipoDeMantenimiento(int id, TipoDeMantenimiento tipo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", tipo);
            return response.IsSuccessStatusCode;
        }

        // Eliminar un tipo de mantenimiento por ID
        public async Task<bool> EliminarTipoDeMantenimiento(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
