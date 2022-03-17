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

        [HttpPost]
        public async Task<ActionResult<ServiceResposta<bool>>> FacerPedido()
        {
            var resultado = await _pedidoService.FacerPedido();
            return Ok(resultado);
        }

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
