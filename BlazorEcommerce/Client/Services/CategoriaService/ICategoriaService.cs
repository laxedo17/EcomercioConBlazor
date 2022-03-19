namespace BlazorEcommerce.Client.Services.CategoriaService
{
    public interface ICategoriaService
    {
        //por se se agregan novos componentes que o cliente sexa notificado
        event Action OnChange;
        List<Categoria> Categorias { get; set; }
        List<Categoria> AdminCategorias { get; set; }
        Task GetCategorias();
        Task GetAdminCategorias();
        Task AddCategoria(Categoria categoria);
        Task UpdateCategoria(Categoria categoria);
        Task DeleteCategoria(int categoriaId);
        Categoria CreateNovaCategoria();
    }
}
