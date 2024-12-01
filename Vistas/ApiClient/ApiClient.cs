using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    // Constructor que acepta la URL base para cada tipo de entidad
    public ApiClient()
    {
        _httpClient = new HttpClient();
    }

    #region CRUD Genérico

    // Obtener todos los registros
    public async Task<List<T>> ObtenerTodos<T>(string baseUrl)
    {
        var response = await _httpClient.GetAsync(baseUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(responseBody);
        }
        else
        {
            return null;
        }
    }

    // Obtener un solo registro por ID
    public async Task<T> ObtenerPorId<T>(string baseUrl, int id)
    {
        var response = await _httpClient.GetAsync($"{baseUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
        else
        {
            return default;
        }
    }

    // Crear un nuevo registro
    public async Task<bool> Crear<T>(string baseUrl, T entidad)
    {
        string json = JsonConvert.SerializeObject(entidad);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(baseUrl, content);
        return response.IsSuccessStatusCode;
    }

    // Actualizar un registro existente
    public async Task<bool> Actualizar<T>(string baseUrl, int id, T entidad)
    {
        string json = JsonConvert.SerializeObject(entidad);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{baseUrl}/{id}", content);
        return response.IsSuccessStatusCode;
    }

    // Eliminar un registro por ID
    public async Task<bool> Eliminar(string baseUrl, int id)
    {
        var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }

    #endregion
}
