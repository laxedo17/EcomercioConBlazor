namespace BlazorEcommerce.Server.Services.CarroService
{
    public interface ICarroService
    {
        Task<ServiceResposta<List<CarroProductoRespostaDto>>> GetCarroProductos(List<CarroItem> carroItems);
    }
}
