namespace BlazorEcommerce.Client.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public CarroService(ILocalStorageService localStorage, HttpClient http, IAuthService authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;
        }
        public event Action OnChange;

        public async Task AddToCarro(CarroItem carroItem)
        {
            if (await _authService.IsUsuarioAuthenticated())
            {
                //Console.WriteLine("usuario autenticado");
                await _http.PostAsJsonAsync("api/carro/add", carroItem);
            }
            else
            {
                //Console.WriteLine("usuario non autenticado");
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
            }

            await GetCarroItemsCount(); //en ambos casos, sexa ou non usuario autenticado, refrescamos a conta de elementos do carro actuales
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
            if (await _authService.IsUsuarioAuthenticated())
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
            if (await _authService.IsUsuarioAuthenticated())
            {
                await _http.DeleteAsync($"api/carro/{productoId}/{productoTypeId}");
            }
            else
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
                }
            }
            //non o necesitamos isto porque temos no Carro.razor un metodo que lee os obxetos que hai no carro await GetCarroItemsCount();
        }

        public async Task UpdateCantidade(CarroProductoRespostaDto producto)
        {
            if (await _authService.IsUsuarioAuthenticated())
            {
                var request = new CarroItem
                {
                    ProductoId = producto.ProductoId,
                    Cantidade = producto.Cantidade,
                    ProductoTypeId = producto.ProductoTypeId
                };

                await _http.PutAsJsonAsync("api/carro/update-cantidade", request);
            }
            else
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
            if (await _authService.IsUsuarioAuthenticated())
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
