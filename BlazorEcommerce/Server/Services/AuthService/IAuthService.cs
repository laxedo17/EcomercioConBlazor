namespace BlazorEcommerce.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResposta<int>> Register(Usuario usuario, string password);
        Task<bool> UsuarioExiste(string email);
        Task<ServiceResposta<string>> Login(string email, string password);
        Task<ServiceResposta<bool>> ChangePassword(int usuarioId, string novoPassword);
        int GetUsuarioId();
    }
}
