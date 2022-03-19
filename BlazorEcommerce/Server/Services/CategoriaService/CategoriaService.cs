namespace BlazorEcommerce.Server.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DataContext _context;

        public CategoriaService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResposta<List<Categoria>>> GetCategorias()
        {
            var categorias = await _context.Categorias
            .Where(c => !c.Deleted && c.Visible)
            .ToListAsync();
            return new ServiceResposta<List<Categoria>>
            {
                Data = categorias
            };
            //.Where(c => !c.Deleted && c.Visible) significa que veremos as categorias que non esten borradas e sexan Visible
        }

        public async Task<ServiceResposta<List<Categoria>>> GetAdminCategorias()
        {
            var categorias = await _context.Categorias
            .Where(c => !c.Deleted)
            .ToListAsync();
            return new ServiceResposta<List<Categoria>>
            {
                Data = categorias
            };
        }


        public async Task<ServiceResposta<List<Categoria>>> AddCategoria(Categoria categoria)
        {
            categoria.Editar = categoria.IsNew = false;
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return await GetAdminCategorias();
        }

        public async Task<ServiceResposta<List<Categoria>>> DeleteCategoria(int id)
        {
            Categoria categoria = await GetCategoriaPorId(id);
            if (categoria == null)
            {
                return new ServiceResposta<List<Categoria>>
                {
                    Exito = false,
                    Mensaxe = "Categoria non atopada"
                };
            }
            else
            {
                categoria.Deleted = true;
                await _context.SaveChangesAsync();

                return await GetAdminCategorias();
            }
        }

        private async Task<Categoria> GetCategoriaPorId(int id)
        {
            //Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ServiceResposta<List<Categoria>>> UpdateCategoria(Categoria categoria)
        {
            var dbCategoria = await GetCategoriaPorId(categoria.Id);
            if (dbCategoria == null)
            {
                return new ServiceResposta<List<Categoria>>
                {
                    Exito = false,
                    Mensaxe = "Categoria non atopada"
                };
            }
            else
            {
                dbCategoria.Nome = categoria.Nome;
                dbCategoria.Url = categoria.Url;
                dbCategoria.Visible = categoria.Visible;

                await _context.SaveChangesAsync();

                return await GetAdminCategorias();

            }
        }
    }
}
