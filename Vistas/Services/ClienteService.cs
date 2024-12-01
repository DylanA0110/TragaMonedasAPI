using SharedModels.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class ClienteService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/cliente"; // URL base de la API de clientes

        public ClienteService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _apiClient.ObtenerTodos<Cliente>(_baseUrl);
        }

        // Obtener un cliente por ID
        public async Task<Cliente> ObtenerClientePorId(int id)
        {
            return await _apiClient.ObtenerPorId<Cliente>(_baseUrl, id);
        }

        // Crear un nuevo cliente
        public async Task<bool> CrearCliente(Cliente cliente)
        {
            return await _apiClient.Crear(_baseUrl, cliente);
        }

        // Actualizar un cliente
        public async Task<bool> ActualizarCliente(int id, Cliente cliente)
        {
            return await _apiClient.Actualizar(_baseUrl, id, cliente);
        }

        // Eliminar un cliente por ID
        public async Task<bool> EliminarCliente(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }

}
