namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResposta<int>> Register(UsuarioRegister request);
    }
}
