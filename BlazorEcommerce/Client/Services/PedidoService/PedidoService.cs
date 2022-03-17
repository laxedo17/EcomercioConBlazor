using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Services.PedidoService
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public PedidoService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        private async Task<bool> IsUsuarioAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<string> FacerPedido()
        {
            if (await IsUsuarioAuthenticated())
            {
                //await _http.PostAsync("api/pedido", null);
                var resultado = await _http.PostAsync("api/pago/checkout", null);
                var url=await resultado.Content.ReadAsStringAsync();
                return url;
            }
            else
            {
                return "login";
               // _navigationManager.NavigateTo("login"); //se o usuario non esta autenticado volvemos a paxina de login
            }
        }

        public async Task<List<PedidoResumenRespostaDto>> GetPedidos()
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<List<PedidoResumenRespostaDto>>>("api/pedido");
            return resultado.Data;
        }

        public async Task<PedidoDetallesRespostaDto> GetPedidoDetalles(int pedidoId)
        {
            var resultado = await _http.GetFromJsonAsync<ServiceResposta<PedidoDetallesRespostaDto>>($"api/pedido/{pedidoId}");
            return resultado.Data;
        }
    }
}
