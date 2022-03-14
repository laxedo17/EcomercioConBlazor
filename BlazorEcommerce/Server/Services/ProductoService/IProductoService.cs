namespace BlazorEcommerce.Server.Services.ProductoService
{
    public interface IProductoService
    {
        Task<ServiceResposta<List<Producto>>> GetProductosAsync();
        Task<ServiceResposta<Producto>> GetProductoAsync(int productoId);
        Task<ServiceResposta<List<Producto>>> GetProductosPorCategoriaAsync(string categoriaUrl);
        //Task<ServiceResposta<List<Producto>>> SearchProductos(string busquedaText); modificado abaixo para usar a Dto que creamos
        Task<ServiceResposta<ProductoSearchResultsDto>> SearchProductos(string busquedaText, int paxina);
        Task<ServiceResposta<List<string>>> GetProductosSearchSuxerencias(string busquedaText);
        Task<ServiceResposta<List<Producto>>> GetProductosDestacados();
    }
}
