namespace BlazorEcommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResposta<int>> Register(UsuarioRegister request)
        {
            var resultado = await _http.PostAsJsonAsync("api/auth/register", request);
            return await resultado.Content.ReadFromJsonAsync<ServiceResposta<int>>();
        }
    }
}
