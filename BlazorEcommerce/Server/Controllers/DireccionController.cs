using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccionService _direccionService;

        public DireccionController(IDireccionService direccionService)
        {
            _direccionService = direccionService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResposta<Direccion>>> GetDireccion()
        {
            return await _direccionService.GetDireccion();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResposta<Direccion>>> AddOuUpdateDireccion(Direccion direccion)
        {
            return await _direccionService.AddOuUpdateDireccion(direccion);
        }
    }
}
