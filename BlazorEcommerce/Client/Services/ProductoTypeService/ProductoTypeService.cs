namespace BlazorEcommerce.Client.Services.ProductoTypeService
{
    public class ProductoTypeService : IProductoTypeService
    {
        private readonly HttpClient _http;
        public List<ProductoType> ProductoTypes { get; set; } = new List<ProductoType>();

        public event Action OnChange;

        public ProductoTypeService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetProductoTypes()
        {
            //a url e como no Controller, e dicir productotype
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<ProductoType>>>("api/productotype");
            ProductoTypes = resultado.Data;
        }

        public async Task AddProductoType(ProductoType productoType)
        {
            var resposta = await _http.PostAsJsonAsync("api/productotype", productoType);
            ProductoTypes = (await resposta.Content.ReadFromJsonAsync<ServiceResposta<List<ProductoType>>>()).Data;
            OnChange.Invoke();
        }

        public ProductoType CreateNovoProductoType()
        {
            var novoProductoType = new ProductoType { IsNew = true, Editar = true };
            ProductoTypes.Add(novoProductoType);
            OnChange.Invoke();
            return novoProductoType;
        }

        public async Task UpdateProductoType(ProductoType productoType)
        {
            var resposta = await _http.PutAsJsonAsync("api/productotype", productoType);
            ProductoTypes = (await resposta.Content.ReadFromJsonAsync<ServiceResposta<List<ProductoType>>>()).Data;
            OnChange.Invoke();
        }
    }
}
