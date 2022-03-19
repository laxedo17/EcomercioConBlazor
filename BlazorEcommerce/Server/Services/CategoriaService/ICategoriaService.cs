namespace BlazorEcommerce.Server.Services.CategoriaService
{
    public interface ICategoriaService
    {
        Task<ServiceResposta<List<Categoria>>> GetCategorias();
        Task<ServiceResposta<List<Categoria>>> GetAdminCategorias();
        Task<ServiceResposta<List<Categoria>>> AddCategoria(Categoria categoria);
        Task<ServiceResposta<List<Categoria>>> UpdateCategoria(Categoria categoria);
        Task<ServiceResposta<List<Categoria>>> DeleteCategoria(int id);
    }
}
