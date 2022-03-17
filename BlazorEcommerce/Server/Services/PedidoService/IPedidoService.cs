namespace BlazorEcommerce.Server.Services.PedidoService
{
    public interface IPedidoService
    {
        Task<ServiceResposta<bool>> FacerPedido();
        Task<ServiceResposta<List<PedidoResumenRespostaDto>>> GetPedidos();
        Task<ServiceResposta<PedidoDetallesRespostaDto>> GetPedidoDetalles(int pedidoId);
    }
}
