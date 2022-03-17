using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.PedidoService
{
    public class PedidoService : IPedidoService
    {
        private readonly DataContext _context;
        private readonly ICarroService _carroService;
        private readonly IAuthService _authService;

        public PedidoService(DataContext context, ICarroService carroService, IAuthService authService)
        {
            _context = context;
            _carroService = carroService;
            _authService = authService;
        }

        //private int GetUsuarioId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResposta<bool>> FacerPedido(int usuarioId)
        {
            var productos = (await _carroService.GetDbCarroProductos(usuarioId)).Data;
            decimal precioTotal = 0;
            productos.ForEach(producto => precioTotal += producto.Precio * producto.Cantidade);

            var pedidoProductos = new List<PedidoProducto>();
            productos.ForEach(producto => pedidoProductos.Add(new PedidoProducto
            {
                ProductoId = producto.ProductoId,
                ProductoTypeId = producto.ProductoTypeId,
                Cantidade = producto.Cantidade,
                PrecioTotal = producto.Precio * producto.Cantidade
            }));

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                PedidoDate = DateTime.Now,
                PrecioTotal = precioTotal,
                PedidoProductos = pedidoProductos
            };

            _context.Pedidos.Add(pedido);

            _context.CarroItems.RemoveRange(_context.CarroItems
                .Where(cp => cp.UsuarioId == usuarioId)); //unha vez o usuario termina o pedido, eliminamos o pedido da cesta

            await _context.SaveChangesAsync();

            return new ServiceResposta<bool> { Data = true };
        }

        public async Task<ServiceResposta<List<PedidoResumenRespostaDto>>> GetPedidos()
        {
            var resposta = new ServiceResposta<List<PedidoResumenRespostaDto>>();
            var pedidos = await _context.Pedidos
                .Include(p => p.PedidoProductos)
                .ThenInclude(pepr => pepr.Producto)
                .Where(p => p.UsuarioId == _authService.GetUsuarioId())
                .OrderByDescending(p => p.PedidoDate)
                .ToListAsync();

            var pedidoResposta = new List<PedidoResumenRespostaDto>();
            pedidos.ForEach(p => pedidoResposta.Add(new PedidoResumenRespostaDto
            {
                Id = p.Id,
                PedidoDate = p.PedidoDate,
                PrecioTotal = p.PrecioTotal,
                Producto = p.PedidoProductos.Count > 1 ?
                $"{p.PedidoProductos.First().Producto.Titulo} e " +
                $"{p.PedidoProductos.Count - 1} mais..." :
                p.PedidoProductos.First().Producto.Titulo,
                ProductoImaxeUrl = p.PedidoProductos.First().Producto.ImaxeUrl
            }));

            resposta.Data = pedidoResposta;

            return resposta;
        }

        public async Task<ServiceResposta<PedidoDetallesRespostaDto>> GetPedidoDetalles(int pedidoId)
        {
            var resposta = new ServiceResposta<PedidoDetallesRespostaDto>();
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProductos) //por cada pedido con productos
                .ThenInclude(pepr => pepr.Producto) //enton incluimos cada producto
                .Include(p => p.PedidoProductos) //volvemos a observar o que temos en cada pedido
                .ThenInclude(pepr => pepr.ProductoType) //e desta vez incluimos o tipo de producto
                .Where(p => p.UsuarioId == _authService.GetUsuarioId() && p.Id == pedidoId)
                .OrderByDescending(p => p.PedidoDate)
                .FirstOrDefaultAsync();

            if (resposta == null)
            {
                resposta.Exito = false;
                resposta.Mensaxe = "Pedido non atopado";
                return resposta;
            }

            var pedidoDetallesResposta = new PedidoDetallesRespostaDto
            {
                PedidoDate = pedido.PedidoDate,
                PrecioTotal = pedido.PrecioTotal,
                Productos = new List<PedidoDetallesProductoRespostaDto>()
            };

            pedido.PedidoProductos.ForEach(elemento =>
                pedidoDetallesResposta.Productos.Add(new PedidoDetallesProductoRespostaDto
                {
                    ProductoId = elemento.ProductoId,
                    ImaxeUrl = elemento.Producto.ImaxeUrl,
                    ProductoType = elemento.ProductoType.Nome,
                    Cantidade = elemento.Cantidade,
                    Titulo = elemento.Producto.Titulo,
                    PrecioTotal = elemento.PrecioTotal
                }));

            resposta.Data = pedidoDetallesResposta;
            return resposta;
        }
    }
}
