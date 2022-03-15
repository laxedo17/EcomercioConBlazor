using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }

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
            throw new NotImplementedException();
        }

    }
}
