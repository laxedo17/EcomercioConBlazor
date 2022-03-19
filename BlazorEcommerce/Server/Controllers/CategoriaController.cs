using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResposta<List<Categoria>>>> GetCategorias()
        {
            var resultado = await _categoriaService.GetCategorias();
            return Ok(resultado);
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResposta<List<Categoria>>>> GetAdminCategorias()
        {
            var resultado = await _categoriaService.GetAdminCategorias();
            return Ok(resultado);
        }

        [HttpDelete("admin/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResposta<List<Categoria>>>> DeleteCategoria(int id)
        {
            var resultado = await _categoriaService.DeleteCategoria(id);
            return Ok(resultado);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResposta<List<Categoria>>>> AddCategoria(Categoria categoria)
        {
            var resultado = await _categoriaService.AddCategoria(categoria);
            return Ok(resultado);
        }

        [HttpPut("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResposta<List<Categoria>>>> UpdateCategoria(Categoria categoria)
        {
            var resultado = await _categoriaService.UpdateCategoria(categoria);
            return Ok(resultado);
        }
    }
}
