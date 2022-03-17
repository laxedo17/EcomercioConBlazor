namespace BlazorEcommerce.Client.Services.DireccionService
{
    public interface IDireccionService
    {
        Task<Direccion> GetDireccion();
        Task<Direccion> AddOuUpdateDireccion(Direccion direccion);
    }
}
