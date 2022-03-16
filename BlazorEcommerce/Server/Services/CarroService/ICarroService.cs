namespace BlazorEcommerce.Server.Services.CarroService
{
    public interface ICarroService
    {
        Task<ServiceResposta<List<CarroProductoRespostaDto>>> GetCarroProductos(List<CarroItem> carroItems);
        Task<ServiceResposta<List<CarroProductoRespostaDto>>> GardarItemsCarro(List<CarroItem> carroItems);
        // sen refactorizar Task<ServiceResposta<List<CarroProductoRespostaDto>>> GardarItemsCarro(List<CarroItem> carroItems, int usuarioId);
        Task<ServiceResposta<int>> GetCarroItemsCount();
        Task<ServiceResposta<List<CarroProductoRespostaDto>>> GetDbCarroProductos();
    }
}
