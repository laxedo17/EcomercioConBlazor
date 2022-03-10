namespace BlazorEcommerce.Server.Services.CategoriaService
{
    public interface ICategoriaService
    {
        Task<ServiceResposta<List<Categoria>>> GetCategoriasAsync();

    }
}
