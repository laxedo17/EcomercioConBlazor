namespace BlazorEcommerce.Client.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CarroService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public event Action OnChange;

        public async Task AddToCarro(CarroItem carroItem)
        {
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
            OnChange.Invoke();
        }

        public async Task<List<CarroItem>> GetCarroItems()
        {
            var carro = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
            if (carro == null)
            {
                carro = new List<CarroItem>();
            }

            return carro;
        }

        public async Task<List<CarroProductoRespostaDto>> GetCarroProductos()
        {
            var carroItems = await _localStorage.GetItemAsync<List<CarroItem>>("carro");
            var resposta = await _http.PostAsJsonAsync("api/carro/productos", carroItems);
            //como a resposta e http, temos que modificar alguns detalles, o cal se pode ver no codigo de abaixo
            var carroProductos =
                await resposta.Content.ReadFromJsonAsync<ServiceResposta<List<CarroProductoRespostaDto>>>();
            return carroProductos.Data;
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
                OnChange.Invoke();
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
    }
}
