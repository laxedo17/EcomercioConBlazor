namespace BlazorEcommerce.Client.Services.ProductoService
{
    public interface IProductoService
    {
        List<Producto> Productos { get; set; }
        Task GetProductos();
        Task<ServiceResposta<Producto>> GetProducto(int productoId);
    }
}
