using System.Net.Http.Json;
using RutasCompartidas.Domain.Entities;

namespace RutasCompartidas.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiRutas");
        }

        public async Task<Usuario?> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", new Usuario
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<string?> RegistroAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/registro", usuario);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                return "Este correo ya está registrado.";

            return response.IsSuccessStatusCode ? null : "Error al registrar usuario.";
        }
    }
}
