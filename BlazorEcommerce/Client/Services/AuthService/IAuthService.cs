namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResposta<int>> Register(UsuarioRegister request);
        Task<ServiceResposta<string>> Login(UserLogin request);
        Task<ServiceResposta<bool>> ChangePassword(UsuarioCambiaPassword request);
    }
}
