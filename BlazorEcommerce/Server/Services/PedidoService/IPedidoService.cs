namespace BlazorEcommerce.Server.Services.PedidoService
{
    public interface IPedidoService
    {
        Task<ServiceResposta<bool>> FacerPedido(int usuarioId);
        Task<ServiceResposta<List<PedidoResumenRespostaDto>>> GetPedidos();
        Task<ServiceResposta<PedidoDetallesRespostaDto>> GetPedidoDetalles(int pedidoId);
    }
}
