using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace BlazorEcommerce.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUsuarioId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResposta<int>> Register(Usuario usuario, string password)
        {
            if (await UsuarioExiste(usuario.Email))
            {
                return new ServiceResposta<int>
                {
                    Exito = false,
                    Mensaxe = "Xa existe un usuario con esta conta de email"
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new ServiceResposta<int> { Data = usuario.Id, Mensaxe = "Rexistro exitoso!" }; //devolvemos unha ServiceResposta na cal enviamos os datos a unha id de Usuario
        }

        public async Task<bool> UsuarioExiste(string email)
        {
            if (await _context.Usuarios.AnyAsync(usuario => usuario.Email.ToLower()
                    .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<ServiceResposta<string>> Login(string email, string password)
        {
            var resposta = new ServiceResposta<string>();
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.Email.ToLower().Equals(email.ToLower()));
            if (usuario == null)
            {
                resposta.Exito = false;
                resposta.Mensaxe = "Usuario non atopado";
            }
            else if (!VerificarPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                resposta.Exito = false;
                resposta.Mensaxe = "Password erroneo";
            }
            else
            {
                resposta.Data = CreateToken(usuario);
            }


            return resposta;
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string? CreateToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResposta<bool>> ChangePassword(int usuarioId, string novoPassword)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return new ServiceResposta<bool>
                {
                    Exito = false,
                    Mensaxe = "Usuario non atopado"
                };
            }

            CreatePasswordHash(novoPassword, out byte[] passwordHash, out byte[] passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResposta<bool> { Data = true, Mensaxe = "Modificouse o password" };
        }
    }
}
