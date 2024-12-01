using SharedModels;
using SharedModels.SharedModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vistas.Services
{
    public class DepartamentoService
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseUrl = "https://localhost:7052/api/departamento"; // URL base de la API de departamentos

        public DepartamentoService()
        {
            _apiClient = new ApiClient();
        }

        // Obtener todos los departamentos
        public async Task<List<Departamento>> ObtenerDepartamentos()
        {
            return await _apiClient.ObtenerTodos<Departamento>(_baseUrl);
        }

        // Obtener un departamento por ID
        public async Task<Departamento> ObtenerDepartamentoPorId(int id)
        {
            return await _apiClient.ObtenerPorId<Departamento>(_baseUrl, id);
        }

        // Crear un nuevo departamento
        public async Task<bool> CrearDepartamento(Departamento departamento)
        {
            return await _apiClient.Crear(_baseUrl, departamento);
        }

        // Actualizar un departamento
        public async Task<bool> ActualizarDepartamento(int id, Departamento departamento)
        {
            return await _apiClient.Actualizar(_baseUrl, id, departamento);
        }

        // Eliminar un departamento por ID
        public async Task<bool> EliminarDepartamento(int id)
        {
            return await _apiClient.Eliminar(_baseUrl, id);
        }
    }
}
