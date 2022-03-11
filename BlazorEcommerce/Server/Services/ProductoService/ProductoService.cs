namespace BlazorEcommerce.Server.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private readonly DataContext _context;

        public ProductoService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResposta<Producto>> GetProductoAsync(int productoId)
        {
            var resposta = new ServiceResposta<Producto>();//inicializamos a resposta
            //var producto = await _context.Productos.FindAsync(productoId).ConfigureAwait(false);

            //cambiamos codigo anterior para engadir Include e ter as relacions compostas por mais de unha id agregadas
            var producto = await _context.Productos
                .Include(p => p.Variantes) //por cada producto incluimos as variantes
                .ThenInclude(v => v.ProductoType) //por cada variante queremos tamen incluir o tipo de producto
                .FirstOrDefaultAsync(p => p.Id == productoId);//obtemos o producto apropiado -producto unico- da base de datos
            if (producto == null)
            {
                resposta.Exito = false;
                resposta.Mensaxe = "Sentimolo moito, este producto non existe na nosa tenda chic";
            }
            else
            {
                resposta.Data = producto;
            }

            return resposta;
        }

        // public async Task<ServiceResposta<List<Producto>>> GetProductosAsync()
        // {
        //     var resposta = new ServiceResposta<List<Producto>>
        //     {
        //         Data = await _context.Productos.ToListAsync()
        //     };

        //     return resposta;
        // }

        /// <summary>
        /// Metodo modificado con variantes de productos incluidos, sen incluir os tipos de producto porque non se mostraran ao cliente, simplemente se inclue para unha lista de productos, para mostrar detalles temos GetProductoAsync()
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResposta<List<Producto>>> GetProductosAsync()
        {
            var resposta = new ServiceResposta<List<Producto>>
            {
                Data = await _context.Productos.Include(p => p.Variantes).ToListAsync()
            };

            return resposta;
        }

        // public async Task<ServiceResposta<List<Producto>>> GetProductosPorCategoriaAsync(string categoriaUrl)
        // {
        //     var resposta = new ServiceResposta<List<Producto>>
        //     {
        //         Data = await _context.Productos
        //             .Where(p => p.Categoria.Url.ToLower().Equals(categoriaUrl.ToLower()))
        //             .ToListAsync()
        //     };

        //     return resposta;
        // }

        public async Task<ServiceResposta<List<Producto>>> GetProductosPorCategoriaAsync(string categoriaUrl)
        {
            var resposta = new ServiceResposta<List<Producto>>
            {
                Data = await _context.Productos
                    .Where(p => p.Categoria.Url.ToLower().Equals(categoriaUrl.ToLower()))
                    .Include(p => p.Variantes)
                    .ToListAsync()
            };

            return resposta;
        }

        /// <summary>
        /// Metodo para busqueda de texto na web, no nome dos productos e na descripcion
        /// </summary>
        /// <param name="busquedaText"></param>
        /// <returns></returns>
        public async Task<ServiceResposta<List<Producto>>> SearchProductos(string busquedaText)
        {
            var resposta = new ServiceResposta<List<Producto>>
            {
                Data = await FindProductosPorBusquedaTexto(busquedaText)
            };
            return resposta; //tras convertir o anterior a unha lista, devolvemos todo
        }

        /// <summary>
        /// Metodo extraido de SearchProductos
        /// </summary>
        /// <param name="busquedaText"></param>
        /// <returns></returns>
        private async Task<List<Producto>> FindProductosPorBusquedaTexto(string busquedaText)
        {
            return await _context.Productos
                            .Where(p => p.Titulo.ToLower().Contains(busquedaText.ToLower())
                            ||
                            p.Descripcion.ToLower().Contains(busquedaText.ToLower()))
                                .Include(p => p.Variantes)
                                .ToListAsync();
        }

        /// <summary>
        /// Metodo de busqueda de productos pero algo mais complexo utilizando un string en lugar de un producto
        /// </summary>
        /// <param name="busquedaText"></param>
        /// <returns></returns>
        public async Task<ServiceResposta<List<string>>> GetProductosSearchSuxerencias(string busquedaText)
        {
            //primeiro necesitamos obter os datos como no metodo SearchProductos()
            //extraemos o metodo FindProductoPorBusquedaTexto que estaba basado no contido entre corchetes no metodo SearchProductos
            var productos = await FindProductosPorBusquedaTexto(busquedaText);
            List<string> resultado = new List<string>();

            //por cada producto que atopamos, comprobamos que o titulo conten o noso termino de busca, e non solo a descripcion
            //se ese e o caso, queremos engadir o titulo completo do producto as suxerencias
            //asi a persona pode ver o titulo apropiado, coa escritura apropiada, e con un simple click 
            //e enton pedirase unha busqueda extra de novo, pero resultara no producto final
            foreach (var producto in productos)
            {
                if (producto.Titulo.Contains(busquedaText, StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Add(producto.Titulo); //no caso de que unha busqueda a conteña o titulo, engadimos dito titulo aos nosos resultados
                }

                //agora que temos o titulo
                //tamen queremos que nos aparezan as suxerencias dependendo da descripcion
                if (producto.Descripcion != null)
                {
                    //traballamos cos signos de puntuacion da nosa descripcion
                    //e despois de ahi podemos crear listas ou un array de todas as palabras que estan na nosa descripcion
                    //e finalmente por cada palabra na nosa lista
                    //comprobamos que cada palabra conten o texto de busqueda e se e o caso, engadimola a nosa lista de resultados
                    var puntuacion = producto.Descripcion.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var palabras = producto.Descripcion.Split()
                        .Select(s => s.Trim(puntuacion)); //de arriba seleccionamos todo e quitamos os signos de puntuacion (puntos, interrogacions, etc)

                    //agora recorremos todas as palabras que obtivemos
                    foreach (var palabra in palabras)
                    {
                        if (palabra.Contains(busquedaText, StringComparison.OrdinalIgnoreCase) && !resultado.Contains(palabra))
                        {
                            resultado.Add(palabra); //engadese a palabra a lista de resultados, se a palabra non estaba na lista (paso importante)
                        }
                    }
                }
            }


            return new ServiceResposta<List<string>> { Data = resultado };
        }
    }
}
