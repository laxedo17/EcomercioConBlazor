using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CarroService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        //refactorizamos o codigo para usar un pequeno metodo que nos permitira obter a Id de usuario con httpContextAccessor e asi facer o codigo mais simple no metodo GardarItemsCarro
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
            carroItems.ForEach(carroItem => carroItem.UsuarioId = _authService.GetUsuarioId());
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
            var conta = (await _context.CarroItems.Where(ci => ci.UsuarioId == _authService.GetUsuarioId()).ToListAsync()).Count;
            return new ServiceResposta<int> { Data = conta };
        }

        public async Task<ServiceResposta<List<CarroProductoRespostaDto>>> GetDbCarroProductos()
        {
            return await GetCarroProductos(await _context.CarroItems
                .Where(ci => ci.UsuarioId == _authService.GetUsuarioId()).ToListAsync());
        }

        public async Task<ServiceResposta<bool>> AddToCarro(CarroItem carroItem)
        {
            carroItem.UsuarioId = _authService.GetUsuarioId();

            var mismoItem = await _context.CarroItems
                .FirstOrDefaultAsync(ci => ci.ProductoId == carroItem.ProductoId &&
                ci.ProductoTypeId == carroItem.ProductoTypeId &&
                ci.UsuarioId == carroItem.UsuarioId);

            if (mismoItem == null)
            {
                _context.CarroItems.Add(carroItem);
            }
            //se atopamos o item do carro coa misma id, enton sumamos a cantidade ao carro
            else
            {
                mismoItem.Cantidade += carroItem.Cantidade;
            }

            await _context.SaveChangesAsync();
            return new ServiceResposta<bool> { Data = true };
        }

        public async Task<ServiceResposta<bool>> UpdateCantidade(CarroItem carroItem)
        {
            var dbCarroItem = await _context.CarroItems
                .FirstOrDefaultAsync(ci => ci.ProductoId == carroItem.ProductoId &&
                ci.ProductoTypeId == carroItem.ProductoTypeId &&
                ci.UsuarioId == _authService.GetUsuarioId());

            if (dbCarroItem == null)
            {
                return new ServiceResposta<bool>
                {
                    Data = false,
                    Exito = false,
                    Mensaxe = "O elemento do carro non existe"
                };
            }

            dbCarroItem.Cantidade = carroItem.Cantidade;
            await _context.SaveChangesAsync();
            return new ServiceResposta<bool> { Data = true };
        }

        public async Task<ServiceResposta<bool>> RemoveItemDeCarro(int productoId, int productoTypeId)
        {
            var dbCarroItem = await _context.CarroItems
                .FirstOrDefaultAsync(ci => ci.ProductoId == productoId &&
                ci.ProductoTypeId == productoTypeId &&
                ci.UsuarioId == _authService.GetUsuarioId());

            if (dbCarroItem == null)
            {
                return new ServiceResposta<bool>
                {
                    Data = false,
                    Exito = false,
                    Mensaxe = "O elemento do carro non existe"
                };
            }
            //si encontramos o item actual
            _context.CarroItems.Remove(dbCarroItem);
            await _context.SaveChangesAsync();

            return new ServiceResposta<bool> { Data = true };
        }
    }
}