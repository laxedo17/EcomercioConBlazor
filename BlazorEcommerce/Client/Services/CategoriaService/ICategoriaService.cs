namespace BlazorEcommerce.Client.Services.CategoriaService
{
    public interface ICategoriaService
    {
        List<Categoria> Categorias { get; set; }
        Task GetCategoriasAsync();
    }
}
