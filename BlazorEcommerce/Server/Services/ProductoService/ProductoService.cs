namespace BlazorEcommerce.Server.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private readonly DataContext _context;

        public ProductoService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResposta<Producto>> GetProductoAsync(int productoId)
        {
            var resposta = new ServiceResposta<Producto>();//inicializamos a resposta
            var producto = await _context.Productos.FindAsync(productoId).ConfigureAwait(false);//obtemos o producto da base de datos
            if (producto == null)
            {
                resposta.Exito = false;
                resposta.Mensaxe = "Sentimolo moito, este producto non existe na nosa tenda chic";
            }
            else
            {
                resposta.Data = producto;
            }

            return resposta;
        }

        public async Task<ServiceResposta<List<Producto>>> GetProductosAsync()
        {
            var resposta = new ServiceResposta<List<Producto>>
            {
                Data = await _context.Productos.ToListAsync()
            };

            return resposta;
        }
    }
}
