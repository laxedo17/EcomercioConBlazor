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

        public string Mensaxe { get; set; } = "Cargando productos...";
        public event Action ProductosCambiaron;

        public async Task<ServiceResposta<Producto>> GetProducto(int productoId)
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<Producto>>($"api/producto/{productoId}");
            return resultado;
        }

        // public async Task GetProductos()
        // {
        //     var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto");
        //     if (resultado != null && resultado.Data != null)
        //     {
        //         Productos = resultado.Data;
        //     }
        // }

        public async Task GetProductos(string? categoriaUrl = null)
        {
            //Usamos o coalescing operator
            var resultado = categoriaUrl == null ?
            await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto") :
            await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>($"api/producto/categoria/{categoriaUrl}"); //ponhemos caracter $ para poder agregar variables literales
            if (resultado != null && resultado.Data != null)
            {
                Productos = resultado.Data;
            }

            ProductosCambiaron.Invoke(); //cada componente suscrito a este evento fara o que lle indiquemos. Hai que suscribir algun componente a este evento para que non de unha excepcion, por exemplo no component ProductoList.razor 
        }

        public async Task<List<string>> GetProductoSearchSuxerencias(string busquedaText)
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<string>>>($"api/producto/searchsuggestions/{busquedaText}");
            return resultado.Data;
        }

        public async Task SearchProductos(string busquedaText)
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>($"api/producto/search/{busquedaText}");
            if (resultado != null && resultado.Data != null)
            {
                Productos = resultado.Data;
            }

            if (Productos.Count == 0)
            {
                Mensaxe = "Non se atoparon productos";
            }
            ProductosCambiaron?.Invoke();
        }
    }
}