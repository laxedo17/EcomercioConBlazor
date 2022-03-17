using Stripe;
using Stripe.Checkout;

namespace BlazorEcommerce.Server.Services.PagoService
{
    public class PagoService : IPagoService
    {
        private readonly ICarroService _carroService;
        private readonly IAuthService _authService; //usado aqui para a direccion de email
        private readonly IPedidoService _pedidoService; //usaremos mais tarde isto para completar o pedido con Webhooks e gardalo na base de datos

        const string SEGREDO = "whsec_312ea66cbde359622e4fdb2a5e17f7924e7be3721d282dfbd05be6c040e05fd2";

        public PagoService(ICarroService carroService, IAuthService authService, IPedidoService pedidoService)
        {
            StripeConfiguration.ApiKey = "sk_test_51KeIuIHL7gZSeg0uXYwXhLAN2Ibagpu1TAQmMbk9y2F1Vlo5U1g373C4RM0FogYrunb8FMbk63vpzzs4ctajzkPB00mNMx9KtW";
            _carroService = carroService;
            _authService = authService;
            _pedidoService = pedidoService;
        }

        public async Task<ServiceResposta<bool>> CompletarPedido(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    SEGREDO
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var usuario = await _authService.GetUsuarioPorEmail(session.CustomerEmail); //aqui obtemos o noso usuario desde a base de datos
                    await _pedidoService.FacerPedido(usuario.Id);
                }

                return new ServiceResposta<bool> { Data = true };
            }
            catch (StripeException e)
            {
                return new ServiceResposta<bool> { Data = false, Exito = false, Mensaxe = e.Message };
            }
        }

        public async Task<Session> CreateCheckoutSession()
        {
            var productos = (await _carroService.GetDbCarroProductos()).Data;

            //Os productos que vera o usuario cando faga o pago con Stripe
            var lineElementos = new List<SessionLineItemOptions>();
            productos.ForEach(producto => lineElementos.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = producto.Precio * 100,
                    Currency = "eur",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = producto.Titulo,
                        Images = new List<string> { producto.ImaxeUrl }
                    }
                },
                Quantity = producto.Cantidade
            }));


            //Creamos a checkout session
            //Usaremos tarxeta de credito, e con iso poderemos usar Apple Pay ou Google Pay
            //modo de pago usaremos "payment" -pago unico-. Tamen se poderia usar subscription para outro tipo de pago
            var opcions = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUsuarioEmail(),
                ShippingAddressCollection =
                    new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> { "US" }
                    },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineElementos,
                Mode = "payment",
                SuccessUrl = "https://localhost:7263/pedido-exito",
                CancelUrl = "https://localhost:7263/carro"
            };

            var servicio = new SessionService();
            Session session = servicio.Create(opcions);
            return session; //despois o controller agarra esta session e devolve a url de aqui
        }
    }
}
