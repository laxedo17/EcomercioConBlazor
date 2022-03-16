using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResposta<int>>> Register(UsuarioRegister request)
        {
            var resposta = await _authService.Register(
            new Usuario
            {
                Email = request.Email
            },
            request.Password);

            if (!resposta.Exito)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResposta<string>>> Login(UserLogin request)
        {
            var resposta = await _authService.Login(request.Email, request.Password);
            if (!resposta.Exito)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResposta<bool>>> ChangePassword([FromBody] string novoPassword)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resposta = await _authService.ChangePassword(int.Parse(usuarioId), novoPassword);
            if (!resposta.Exito)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }
    }
}
