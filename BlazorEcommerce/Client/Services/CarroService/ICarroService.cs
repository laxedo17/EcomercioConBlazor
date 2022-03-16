namespace BlazorEcommerce.Client.Services.CarroService
{
    public interface ICarroService
    {
        event Action OnChange;
        Task AddToCarro(CarroItem carroItem);
        //Task<List<CarroItem>> GetCarroItems();
        Task<List<CarroProductoRespostaDto>> GetCarroProductos();
        Task RemoveProductoDeCarro(int productoId, int productoTypeId);
        Task UpdateCantidade(CarroProductoRespostaDto producto);
        Task GardarItemsCarro(bool vaciarLocalCarro);
        Task GetCarroItemsCount();
    }
}
