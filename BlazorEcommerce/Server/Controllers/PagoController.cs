using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpPost("checkout"), Authorize]
        public async Task<ActionResult<string>> CreateCheckoutSession()
        {
            var session = await _pagoService.CreateCheckoutSession();
            return Ok(session.Url);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResposta<bool>>> CompletarPedido()
        {
            var resposta = await _pagoService.CompletarPedido(Request);//Request e un http request object, por iso funciona
            if (!resposta.Exito)
            {
                return BadRequest(resposta.Mensaxe);
            }
            return Ok(resposta);
        }
    }
}
