using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        //o metodo que existia aqui antes xa non o necesitamos porque agora UNICAMENTE a webhook deberia facer unha chamada para crear un pedido



        [HttpGet]
        public async Task<ActionResult<ServiceResposta<List<PedidoResumenRespostaDto>>>> GetPedidos()
        {
            var resultado = await _pedidoService.GetPedidos();
            return Ok(resultado);
        }

        [HttpGet("{pedidoId}")]
        public async Task<ActionResult<ServiceResposta<PedidoDetallesRespostaDto>>> GetPedidoDetalles(int pedidoId)
        {
            var resultado = await _pedidoService.GetPedidoDetalles(pedidoId);
            return Ok(resultado);
        }
    }
}
