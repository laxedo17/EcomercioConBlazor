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
        public int PaxinaActual { get; set; } = 1;
        public int ContaPaxinas { get; set; } = 0;
        public string LastBusquedaText { get; set; } = string.Empty;
        public List<Producto> AdminProductos { get; set; } = new List<Producto>();

        public event Action ProductosCambiaron;

        public async Task<Producto> CreateProducto(Producto producto)
        {
            var resultado = await _http.PostAsJsonAsync("api/producto", producto);
            var novoProducto = (await resultado.Content.ReadFromJsonAsync<ServiceResposta<Producto>>()).Data;
            return novoProducto;
        }

        public async Task<Producto> UpdateProducto(Producto producto)
        {
            var resultado = await _http.PutAsJsonAsync($"api/producto", producto);
            return (await resultado.Content.ReadFromJsonAsync<ServiceResposta<Producto>>()).Data;
        }

        public async Task DeleteProducto(Producto producto)
        {
            var resultado = await _http.DeleteAsync($"api/producto/{producto.Id}");
        }

        public async Task GetAdminProductos()
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto/admin");
            AdminProductos = resultado.Data;
            PaxinaActual = 1;
            ContaPaxinas = 0;
            if (AdminProductos.Count == 0)
            {
                Mensaxe = "Non se atoparon productos";
            }
        }

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
            //await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto") :
            //agora so mostraremos productos destacados
            await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto/destacado") :
            await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>($"api/producto/categoria/{categoriaUrl}"); //ponhemos caracter $ para poder agregar variables literales
            if (resultado != null && resultado.Data != null)
            {
                Productos = resultado.Data;
            }

            PaxinaActual = 1;
            ContaPaxinas = 0;

            if (Productos.Count == 0)
            {
                Mensaxe = "Non se atoparon productos";
            }

            ProductosCambiaron.Invoke(); //cada componente suscrito a este evento fara o que lle indiquemos. Hai que suscribir algun componente a este evento para que non de unha excepcion, por exemplo no component ProductoList.razor 
        }

        //metodo orixinal que se modificou arriba para aceptar paxinacion
        /*
                public async Task GetProductos(string? categoriaUrl = null)
                {
                    //Usamos o coalescing operator
                    var resultado = categoriaUrl == null ?
                    //await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto") :
                    //agora so mostraremos productos destacados
                    await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>("api/producto/destacado") :
                    await _http.GetFromJsonAsync<ServiceResposta<List<Producto>>>($"api/producto/categoria/{categoriaUrl}"); //ponhemos caracter $ para poder agregar variables literales
                    if (resultado != null && resultado.Data != null)
                    {
                        Productos = resultado.Data;
                    }

                    ProductosCambiaron.Invoke(); //cada componente suscrito a este evento fara o que lle indiquemos. Hai que suscribir algun componente a este evento para que non de unha excepcion, por exemplo no component ProductoList.razor 
                }
        */

        public async Task<List<string>> GetProductoSearchSuxerencias(string busquedaText)
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<string>>>($"api/producto/searchsuggestions/{busquedaText}");
            return resultado.Data;
        }

        public async Task SearchProductos(string busquedaText, int paxina)
        {
            LastBusquedaText = busquedaText;
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<ProductoSearchResultsDto>>($"api/producto/search/{busquedaText}/{paxina}");
            if (resultado != null && resultado.Data != null)
            {
                Productos = resultado.Data.Productos;
                PaxinaActual = resultado.Data.PaxinaActual;
                ContaPaxinas = resultado.Data.Paxinas;
            }

            if (Productos.Count == 0)
            {
                Mensaxe = "Non se atoparon productos";
            }
            ProductosCambiaron?.Invoke();
        }

        /*
        Metodo que modificamos enriba para agregar paxinacion
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

        */
    }
}