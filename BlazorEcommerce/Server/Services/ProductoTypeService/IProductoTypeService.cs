namespace BlazorEcommerce.Server.Services.ProductoTypeService
{
    public interface IProductoTypeService
    {
        Task<ServiceResposta<List<ProductoType>>> GetProductoTypes();
        Task<ServiceResposta<List<ProductoType>>> AddProductoType(ProductoType productoType);
        Task<ServiceResposta<List<ProductoType>>> UpdateProductoType(ProductoType productoType);
        //Task<ServiceResposta<ProductoType>> DeleteProductoType(int id);
    }
}
