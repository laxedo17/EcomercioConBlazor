namespace BlazorEcommerce.Server.Services.ProductoTypeService
{
    public class ProductoTypeService : IProductoTypeService
    {
        private readonly DataContext _context;

        public ProductoTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResposta<List<ProductoType>>> GetProductoTypes()
        {
            var productoTypes = await _context.ProductoTypes.ToListAsync();
            return new ServiceResposta<List<ProductoType>> { Data = productoTypes };
        }

        public async Task<ServiceResposta<List<ProductoType>>> AddProductoType(ProductoType productoType)
        {
            productoType.Editar = productoType.IsNew = false;
            _context.ProductoTypes.Add(productoType);
            await _context.SaveChangesAsync();
            return await GetProductoTypes();
        }

        public async Task<ServiceResposta<List<ProductoType>>> UpdateProductoType(ProductoType productoType)
        {
            var dbProductoType = await _context.ProductoTypes.FindAsync(productoType.Id);
            if (dbProductoType == null)
            {
                return new ServiceResposta<List<ProductoType>>
                {
                    Exito = false,
                    Mensaxe = "Tipo de producto non atopado"
                };
            }

            dbProductoType.Nome = productoType.Nome;
            await _context.SaveChangesAsync();
            return await GetProductoTypes();
        }
    }
}
