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
            //var producto = await _context.Productos.FindAsync(productoId).ConfigureAwait(false);

            //cambiamos codigo anterior para engadir Include e ter as relacions compostas por mais de unha id agregadas
            var producto = await _context.Productos
                .Include(p => p.Variantes) //por cada producto incluimos as variantes
                .ThenInclude(v => v.ProductoType) //por cada variante queremos tamen incluir o tipo de producto
                .FirstOrDefaultAsync(p => p.Id == productoId);//obtemos o producto apropiado -producto unico- da base de datos
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

        // public async Task<ServiceResposta<List<Producto>>> GetProductosAsync()
        // {
        //     var resposta = new ServiceResposta<List<Producto>>
        //     {
        //         Data = await _context.Productos.ToListAsync()
        //     };

        //     return resposta;
        // }

        /// <summary>
        /// Metodo modificado con variantes de productos incluidos, sen incluir os tipos de producto porque non se mostraran ao cliente, simplemente se inclue para unha lista de productos, para mostrar detalles temos GetProductoAsync()
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResposta<List<Producto>>> GetProductosAsync()
        {
            var resposta = new ServiceResposta<List<Producto>>
            {
                Data = await _context.Productos.Include(p => p.Variantes).ToListAsync()
            };

            return resposta;
        }

        // public async Task<ServiceResposta<List<Producto>>> GetProductosPorCategoriaAsync(string categoriaUrl)
        // {
        //     var resposta = new ServiceResposta<List<Producto>>
        //     {
        //         Data = await _context.Productos
        //             .Where(p => p.Categoria.Url.ToLower().Equals(categoriaUrl.ToLower()))
        //             .ToListAsync()
        //     };

        //     return resposta;
        // }

        public async Task<ServiceResposta<List<Producto>>> GetProductosPorCategoriaAsync(string categoriaUrl)
        {
            var resposta = new ServiceResposta<List<Producto>>
            {
                Data = await _context.Productos
                    .Where(p => p.Categoria.Url.ToLower().Equals(categoriaUrl.ToLower()))
                    .Include(p => p.Variantes)
                    .ToListAsync()
            };

            return resposta;
        }
    }
}
