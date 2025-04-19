using System.Net.Http.Json;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Web.Services
{
    public class RutaApiService
    {
        private readonly HttpClient _httpClient;

        public RutaApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiRutas");
        }

        public async Task<List<Ruta>> ObtenerRutasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ruta>>("rutas") ?? new List<Ruta>();
        }

        public async Task<Ruta?> ObtenerRutaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ruta>($"rutas/{id}");
        }

        public async Task CrearRutaAsync(Ruta ruta)
        {
            await _httpClient.PostAsJsonAsync("rutas", ruta);
        }

        public async Task ActualizarRutaAsync(Ruta ruta)
        {
            await _httpClient.PutAsJsonAsync($"rutas/{ruta.Id}", ruta);
        }

        public async Task EliminarRutaAsync(int id)
        {
            await _httpClient.DeleteAsync($"rutas/{id}");
        }
    }
}
