namespace BlazorEcommerce.Client.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _http;

        public CategoriaService(HttpClient http)
        {
            _http = http;
        }
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();

        public async Task GetCategoriasAsync()
        {
            var resposta = await _http.GetFromJsonAsync<ServiceResposta<List<Categoria>>>("api/categoria");

            if (resposta != null && resposta.Data != null)
            {
                Categorias = resposta.Data;
            }
        }
    }
}
