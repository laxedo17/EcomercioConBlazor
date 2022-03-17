namespace BlazorEcommerce.Client.Services.DireccionService
{
    public class DireccionService : IDireccionService
    {
        private readonly HttpClient _http;

        public DireccionService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Direccion> AddOuUpdateDireccion(Direccion direccion)
        {
            var resposta = await _http.PostAsJsonAsync("api/direccion", direccion);
            return resposta.Content.ReadFromJsonAsync<ServiceResposta<Direccion>>().Result.Data;
        }

        public async Task<Direccion> GetDireccion()
        {
            var resposta = await _http.GetFromJsonAsync<ServiceResposta<Direccion>>("api/direccion");
            return resposta.Data;
        }
    }
}
