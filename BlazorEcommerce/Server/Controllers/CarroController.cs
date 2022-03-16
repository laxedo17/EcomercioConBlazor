using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly ICarroService _carroService;

        public CarroController(ICarroService carroService)
        {
            _carroService = carroService;
        }

        //queremos convertir os nosos items do carro en productos, e agregamos un metodo HttpPost
        //o cal significa que estos obxetos de carro se enviaran ao servicio web a este controller usando o body
        //poderiamos usar o atributte [FromBody] no parametro pero estamos usando un complex type e non nos fai falta neste caso, funciona de primeiras
        //se o parametro fose un string ou un int si poderia ser millor usar [FromBody]
        [HttpPost("productos")]
        public async Task<ActionResult<ServiceResposta<List<CarroProductoRespostaDto>>>> GetCarroProductos(List<CarroItem> carroItems)
        {
            var resultado = await _carroService.GetCarroProductos(carroItems);
            return Ok(resultado);
        }

        [HttpGet("conta")]
        public async Task<ActionResult<ServiceResposta<int>>> GetCarroItemsCount()
        {
            return await _carroService.GetCarroItemsCount();
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResposta<List<CarroProductoRespostaDto>>>> GetDbCarroProductos()
        {
            var resultado = await _carroService.GetDbCarroProductos();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResposta<List<CarroProductoRespostaDto>>>> GardarItemsCarro(List<CarroItem> carroItems)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var resultado = await _carroService.GardarItemsCarro(carroItems);
            return Ok(resultado);
        }

        //sen refactorizar
        /*
                [HttpPost]
        public async Task<ActionResult<ServiceResposta<List<CarroProductoRespostaDto>>>> GardarItemsCarro(List<CarroItem> carroItems)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var resultado = await _carroService.GardarItemsCarro(carroItems, usuarioId);
            return Ok(resultado);
        }
        */
    }
}
