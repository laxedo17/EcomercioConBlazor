namespace BlazorEcommerce.Server.Services.ProductoService
{
    public interface IProductoService
    {
        Task<ServiceResposta<List<Producto>>> GetProductosAsync();
        Task<ServiceResposta<Producto>> GetProductoAsync(int productoId);
        Task<ServiceResposta<List<Producto>>> GetProductosPorCategoriaAsync(string categoriaUrl);
        Task<ServiceResposta<List<Producto>>> SearchProductos(string busquedaText);
        Task<ServiceResposta<List<string>>> GetProductosSearchSuxerencias(string busquedaText);
    }
}
