using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarroService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //refactorizamos o codigo para usar un pequeno metodo que nos permitira obter a Id de usuario con httpContextAccessor e asi facer o codigo mais simple no metodo GardarItemsCarro
        private int GetUsuarioId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
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

        public async Task<ServiceResposta<List<CarroProductoRespostaDto>>> GardarItemsCarro(List<CarroItem> carroItems)
        {
            carroItems.ForEach(carroItem => carroItem.UsuarioId = GetUsuarioId());
            _context.CarroItems.AddRange(carroItems); //AddRange en esencia permite engadir mais obxeto a taboa
            await _context.SaveChangesAsync();
            return await GetDbCarroProductos();

            // return await GetCarroProductos(
            //     await _context.CarroItems
            //     .Where(ci => ci.UsuarioId == GetUsuarioId()).ToListAsync());
        }

        //sen refactorizar
        /*
        public async Task<ServiceResposta<List<CarroProductoRespostaDto>>> GardarItemsCarro(List<CarroItem> carroItems, int usuarioId)
        {
            carroItems.ForEach(carroItem => carroItem.UsuarioId = usuarioId);
            _context.CarroItems.AddRange(carroItems); //AddRange en esencia permite engadir mais obxeto a taboa
            await _context.SaveChangesAsync();

            return await GetCarroProductos(
                await _context.CarroItems
                .Where(ci => ci.UsuarioId == usuarioId).ToListAsync());
        }
        */

        public async Task<ServiceResposta<int>> GetCarroItemsCount()
        {
            var conta = (await _context.CarroItems.Where(ci => ci.UsuarioId == GetUsuarioId()).ToListAsync()).Count;
            return new ServiceResposta<int> { Data = conta };
        }

        public async Task<ServiceResposta<List<CarroProductoRespostaDto>>> GetDbCarroProductos()
        {
            return await GetCarroProductos(await _context.CarroItems
                .Where(ci => ci.UsuarioId == GetUsuarioId()).ToListAsync());
        }
    }
}