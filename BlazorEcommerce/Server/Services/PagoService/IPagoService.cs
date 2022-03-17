using Stripe.Checkout;

namespace BlazorEcommerce.Server.Services.PagoService
{
    public interface IPagoService
    {
        Task<Session> CreateCheckoutSession();
        Task<ServiceResposta<bool>> CompletarPedido(HttpRequest request);
    }
}
