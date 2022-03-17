namespace BlazorEcommerce.Server.Services.DireccionService
{
    public interface IDireccionService
    {
        Task<ServiceResposta<Direccion>> GetDireccion();
        Task<ServiceResposta<Direccion>> AddOuUpdateDireccion(Direccion direccion);
    }
}
