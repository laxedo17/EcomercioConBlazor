namespace BlazorEcommerce.Client.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public CarroService(ILocalStorageService localStorage, HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _localStorage = localStorage;
            _http = http;
            _authStateProvider = authStateProvider;
        }
        public event Action OnChange;

        private async Task<bool> IsUsuarioAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }


        public async Task AddToCarro(CarroItem carroItem)
        {
            if (await IsUsuarioAuthenticated())
            {
                Console.WriteLine("usuario autenticado");
            }
            else
            {
                Console.WriteLine("usuario non autenticado");
            }

            var carro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
            if (carro == null)
            {
                carro = new List<CarroItem>();
            }

            var mismoItem = carro.Find(x => x.ProductoId == carroItem.ProductoId && x.ProductoTypeId == carroItem.ProductoTypeId);
            if (mismoItem == null)
            {
                carro.Add(carroItem);
            }
            else
            {
                mismoItem.Cantidade += carroItem.Cantidade;
            }

            await _localStorage.SetItemAsync("carro", carro);
            await GetCarroItemsCount();
        }

        /*
                public async Task<List<CarroItem>> GetCarroItems()
                {
                    await GetCarroItemsCount();
                    var carro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
                    if (carro == null)
                    {
                        carro = new List<CarroItem>();
                    }

                    return carro;
                }

        */

        public async Task<List<CarroProductoRespostaDto>> GetCarroProductos()
        {
            if (await IsUsuarioAuthenticated())
            {
                var resposta = await _http.GetFromJsonAsync<ServiceResposta<List<CarroProductoRespostaDto>>>("api/carro");
                return resposta.Data;
            }
            else
            {
                var carroItems = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
                if (carroItems == null)
                {
                    return new List<CarroProductoRespostaDto>();
                }
                var resposta = await _http.PostAsJsonAsync("api/carro/productos", carroItems);
                //como a resposta e http, temos que modificar alguns detalles, o cal se pode ver no codigo de abaixo
                var carroProductos =
                    await resposta.Content.ReadFromJsonAsync<ServiceResposta<List<CarroProductoRespostaDto>>>();
                return carroProductos.Data;
            }
        }

        public async Task RemoveProductoDeCarro(int productoId, int productoTypeId)
        {
            var carro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
            if (carro == null)
            {
                return;
            }
            var carroItem = carro.Find(x => x.ProductoId == productoId
                && x.ProductoTypeId == productoTypeId);
            if (carroItem != null)
            {
                carro.Remove(carroItem);
                await _localStorage.SetItemAsync("carro", carro);
                await GetCarroItemsCount();
            }
        }

        public async Task UpdateCantidade(CarroProductoRespostaDto producto)
        {
            var carro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
            if (carro == null)
            {
                return;
            }
            var carroItem = carro.Find(x => x.ProductoId == producto.ProductoId
                && x.ProductoTypeId == producto.ProductoTypeId);
            if (carroItem != null)
            {
                carroItem.Cantidade = producto.Cantidade;
                await _localStorage.SetItemAsync("carro", carro);
            }
        }

        public async Task GardarItemsCarro(bool vaciarLocalCarro = false)
        {
            var localCarro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
            if (localCarro == null)
            {
                return;
            }

            await _http.PostAsJsonAsync("api/carro", localCarro);

            if (vaciarLocalCarro)
            {
                await _localStorage.RemoveItemAsync("carro");
            }
        }

        public async Task GetCarroItemsCount()
        {
            if (await IsUsuarioAuthenticated())
            {
                var resultado = await _http.GetFromJsonAsync<ServiceResposta<int>>("api/carro/conta");
                var conta = resultado.Data;

                await _localStorage.SetItemAsync<int>("carroItemsCount", conta); //gardamos a conta de elementos do carro en localStorage
            }
            else
            {
                var carro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
                await _localStorage.SetItemAsync<int>("carroItemsCount", carro != null ? carro.Count : 0);//obtemos o valor do carro local, observamos se o carro non e null, nese caso establecemos o valor da conta do carro, senon poñemos 0
            }

            OnChange.Invoke(); //chamamos a evento OnChange
        }
    }
}
