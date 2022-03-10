namespace BlazorEcommerce.Server.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DataContext _context;

        public CategoriaService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResposta<List<Categoria>>> GetCategoriasAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return new ServiceResposta<List<Categoria>>
            {
                Data = categorias
            };
        }
    }
}
