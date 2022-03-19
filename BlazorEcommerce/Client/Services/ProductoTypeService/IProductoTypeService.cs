namespace BlazorEcommerce.Client.Services.ProductoTypeService
{
    public interface IProductoTypeService
    {
        event Action OnChange;
        public List<ProductoType> ProductoTypes { get; set; }
        Task GetProductoTypes();
        Task AddProductoType(ProductoType productoType);
        Task UpdateProductoType(ProductoType productoType);
        ProductoType CreateNovoProductoType();
    }
}
