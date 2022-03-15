namespace BlazorEcommerce.Server.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private readonly DataContext _context;

        public CarroService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResposta<List<CarroProductoRespostaDto>>> GetCarroProductos(List<CarroItem> carroItems)
        {
            //establecemos os datos a unha lista de CarroProductoResposta
            var resultado = new ServiceResposta<List<CarroProductoRespostaDto>>
            {
                Data = new List<CarroProductoRespostaDto>()
            };

            //por cada elemento dos elementos solicitados, creamos un novo producto na base de datos
            foreach (var item in carroItems)
            {
                var producto = await _context.Productos
                    .Where(p => p.Id == item.ProductoId)
                    .FirstOrDefaultAsync();

                if (producto == null)
                {
                    continue; //seguimos porque non temos nada que engadir ao carro se o producto e null
                }

                var productoVariante = await _context.ProductoVariantes
                    .Where(v => v.ProductoId == item.ProductoTypeId)
                    .Include(v => v.ProductoType)
                    .FirstOrDefaultAsync();

                if (productoVariante == null)
                {
                    continue;
                }

                var carroProducto = new CarroProductoRespostaDto
                {
                    ProductoId = producto.Id,
                    Titulo = producto.Titulo,
                    ImaxeUrl = producto.ImaxeUrl,
                    Precio = productoVariante.Precio,
                    ProductoType = productoVariante.ProductoType.Nome,
                    ProductoTypeId = productoVariante.ProductoTypeId,
                    Cantidade = item.Cantidade
                };

                resultado.Data.Add(carroProducto);
            }

            return resultado;
        }
    }
}