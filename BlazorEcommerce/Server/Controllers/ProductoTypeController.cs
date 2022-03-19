using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductoTypeController : ControllerBase
    {
        private readonly IProductoTypeService _productoTypeService;

        public ProductoTypeController(IProductoTypeService productoTypeService)
        {
            _productoTypeService = productoTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResposta<List<ProductoType>>>> GetProductoTypes()
        {
            var resposta = await _productoTypeService.GetProductoTypes();
            return Ok(resposta);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResposta<List<ProductoType>>>> AddProductoType(ProductoType productoType)
        {
            var resposta = await _productoTypeService.AddProductoType(productoType);
            return Ok(resposta);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResposta<List<ProductoType>>>> UpdateProductoType(ProductoType productoType)
        {
            var resposta = await _productoTypeService.UpdateProductoType(productoType);
            return Ok(resposta);
        }
    }
}
