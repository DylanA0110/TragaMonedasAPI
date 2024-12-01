using PrototipoAPIBaseDeDatos.SharedModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7052/api/usuarios"; // Update with your actual API URL

        public UsuarioService()
        {
            _httpClient = new HttpClient();
        }

        // Obtener todos los usuarios
        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Usuario>>();
            }
            return null; // Or handle it with custom error handling
        }

        // Obtener un usuario por ID
        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            return null; // Or handle not found error
        }

        // Crear un nuevo usuario
        public async Task<bool> CrearUsuario(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, usuario);
            return response.IsSuccessStatusCode;
        }

        // Actualizar un usuario
        public async Task<bool> ActualizarUsuario(int id, Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

        // Eliminar un usuario por ID
        public async Task<bool> EliminarUsuario(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        // Método para hacer login
        public async Task<Usuario> LoginAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Login", usuario);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(); // Retorna el usuario si la autenticación es exitosa
            }
            else
            {
                // Aquí puedes manejar los errores, como credenciales incorrectas
                return null;
            }
        }
    }
}
