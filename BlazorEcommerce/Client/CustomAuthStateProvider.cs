using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorEcommerce.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identidade = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null; //agora mesmo neste estado o usuario non esta autorizado

            if (!string.IsNullOrEmpty(authToken))
            {
                //se hai un authtoken
                try
                {
                    identidade = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", "")); //con Replace quitamos as comillas do token web obtido. Bearer e o que "leva" o token
                }
                catch
                {
                    await _localStorageService.RemoveItemAsync("authToken"); //se algo falla, o usuario volve a ser desautorizado de novo
                    identidade = new ClaimsIdentity();
                }
            }

            //se o codigo do if se ignora porque non hay authToken no localstorage, enton crearemos un usuario cunha identidade que estara vacia
            //e o usuario non sera autorizado
            var usuario = new ClaimsPrincipal(identidade);
            var estado = new AuthenticationState(usuario);

            NotifyAuthenticationStateChanged(Task.FromResult(estado));

            return estado;
        }

        /// <summary>
        /// Metodo atopado por ahi pero funciona
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer
                .Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            return claims;
        }
    }
}
