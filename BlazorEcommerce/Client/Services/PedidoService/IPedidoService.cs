namespace BlazorEcommerce.Client.Services.PedidoService
{
    public interface IPedidoService
    {
        //Task FacerPedido();
        Task<string> FacerPedido();
        Task<List<PedidoResumenRespostaDto>> GetPedidos();
        Task<PedidoDetallesRespostaDto> GetPedidoDetalles(int pedidoId);
    }
}
