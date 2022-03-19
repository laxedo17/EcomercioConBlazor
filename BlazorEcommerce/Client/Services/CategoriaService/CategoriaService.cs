namespace BlazorEcommerce.Client.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _http;
        public event Action OnChange;

        public CategoriaService(HttpClient http)
        {
            _http = http;
        }
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<Categoria> AdminCategorias { get; set; } = new List<Categoria>();

        public async Task GetCategorias()
        {
            var resposta = await _http.GetFromJsonAsync<ServiceResposta<List<Categoria>>>("api/categoria");

            if (resposta != null && resposta.Data != null)
            {
                Categorias = resposta.Data;
            }
        }

        public async Task GetAdminCategorias()
        {
            var resposta = await _http.GetFromJsonAsync<ServiceResposta<List<Categoria>>>("api/categoria/admin");

            if (resposta != null && resposta.Data != null)
            {
                AdminCategorias = resposta.Data;
            }
        }

        public async Task AddCategoria(Categoria categoria)
        {
            var resposta = await _http.PostAsJsonAsync("api/categoria/admin", categoria);
            AdminCategorias = (await resposta.Content
                .ReadFromJsonAsync<ServiceResposta<List<Categoria>>>()).Data;
            await GetCategorias(); //chamamos a este metodo para ver as categorias incluindo a engadida
            OnChange.Invoke(); //e lanzamos o evento que notifica cambios, algo que necesita saber o noso menu
        }

        public Categoria CreateNovaCategoria()
        {
            var novaCategoria = new Categoria { IsNew = true, Editar = true };
            AdminCategorias.Add(novaCategoria);
            OnChange.Invoke();
            return novaCategoria;
        }

        public async Task DeleteCategoria(int categoriaId)
        {
            var resposta = await _http.DeleteAsync($"api/categoria/admin/{categoriaId}");
            AdminCategorias = (await resposta.Content
                .ReadFromJsonAsync<ServiceResposta<List<Categoria>>>()).Data;
            await GetCategorias(); //chamamos a este metodo para ver as categorias co cambio producido
            OnChange.Invoke(); //e lanzamos o evento que notifica cambios, algo que necesita saber o noso menu
        }

        public async Task UpdateCategoria(Categoria categoria)
        {
            var resposta = await _http.PutAsJsonAsync("api/categoria/admin", categoria);
            AdminCategorias = (await resposta.Content
                .ReadFromJsonAsync<ServiceResposta<List<Categoria>>>()).Data;
            await GetCategorias(); //chamamos a este metodo para ver as categorias incluindo a que modificamos
            OnChange.Invoke(); //e lanzamos o evento que notifica cambios, algo que necesita saber o noso menu
        }
    }
}
