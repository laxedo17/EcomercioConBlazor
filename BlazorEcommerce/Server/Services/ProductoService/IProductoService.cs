namespace BlazorEcommerce.Server.Services.ProductoService
{
    public interface IProductoService
    {
        Task<ServiceResposta<List<Producto>>> GetProductosAsync();
        Task<ServiceResposta<Producto>> GetProductoAsync(int productoId);
    }
}
