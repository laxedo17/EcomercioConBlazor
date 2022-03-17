namespace BlazorEcommerce.Server.Services.DireccionService
{
    public class DireccionService : IDireccionService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public DireccionService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<ServiceResposta<Direccion>> AddOuUpdateDireccion(Direccion direccion)
        {
            var resposta = new ServiceResposta<Direccion>();
            var dbDireccion = (await GetDireccion()).Data;
            //se non existe direccion para ese usuario engadimos a direccion pasada por parametro
            if (dbDireccion == null)
            {
                direccion.UsuarioId = _authService.GetUsuarioId();
                _context.Direccions.Add(direccion);
                resposta.Data = direccion;
            }
            //en caso contrario, actualizamos os datos da direccion
            else
            {
                dbDireccion.Nome = direccion.Nome;
                dbDireccion.Apelidos = direccion.Apelidos;
                dbDireccion.Provincia = direccion.Provincia;
                dbDireccion.Pais = direccion.Pais;
                dbDireccion.Cidade = direccion.Cidade;
                dbDireccion.CP = direccion.CP;
                dbDireccion.Rua = direccion.Rua;
                resposta.Data = dbDireccion;
                //ESENCIAL: Se non establecemos resposta.Data a dbDireccion teremos unha bonita resposta vacia
                //que ven da linha var resposta = new ServiceResposta<Direccion>();
                //xa que a estamos devolvendo abaixo ao final do metodo
                //no metodo cando inicializamos unha direccion si funciona porque resposta.Data = direccion
            }

            await _context.SaveChangesAsync();

            return resposta;
        }

        public async Task<ServiceResposta<Direccion>> GetDireccion()
        {
            int usuarioId = _authService.GetUsuarioId();
            var direccion = await _context.Direccions
                .FirstOrDefaultAsync(d => d.UsuarioId == usuarioId); //obtemos a direccion relacionada con esa Id de usuario
            return new ServiceResposta<Direccion> { Data = direccion };
        }
    }
}
