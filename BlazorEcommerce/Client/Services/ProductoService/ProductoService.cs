namespace BlazorEcommerce.Client.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _http;

        public ProductoService(HttpClient http)
        {
            _http = http;
        }
        public List<Producto> Productos { get; set; } = new List<Producto>();

        public async Task<ServiceResposta<Producto>> GetProducto(int productoId)
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<Producto>>($"api/producto/{productoId}");
            return resultado;
        }

        public async Task GetProductos()
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto");
            if (resultado != null && resultado.Data != null)
            {
                Productos = resultado.Data;
            }

        }
    }
}
