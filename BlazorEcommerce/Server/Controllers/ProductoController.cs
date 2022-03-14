using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        //xa non necesitamos a context porque usamos IProductoService
        // private readonly DataContext _context; //engadido con dependency injection
        // public ProductoController(DataContext context)
        // {
        //     _context = context;
        // }

        /// <summary>
        /// Esta version permite ver o Schema e o modelo da nosa API en Swagger
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResposta<List<Producto>>>> GetProductos() // Paso 21, agregado o servicio creado no proxecto Shared para dar mais datos das operacions que facemos 
        {
            //A parte comentada xa non a necesitamos porque temos o mesmo resultado en GetProductosAsync do obxeto _productoService de IProductoService, este codigo pasouse a aquela clase para maior limpeza no codigo
            //var productos = await _context.Productos.ToListAsync();
            // var resposta = new ServiceResposta<List<Producto>>()
            // {
            //     Data = productos //establecemos Data no valor obtidos dos Productos
            // };
            var resposta = await _productoService.GetProductosAsync(); //pasamos a peticion a traves do Servicio usando aqui _productoService
            return Ok(resposta); // e devolvemos o resultado
        }

        [HttpGet("{productoId}")]
        // pode poñerse asi [Route("{id}")] , pero faremolo en HttpGet
        public async Task<ActionResult<ServiceResposta<Producto>>> GetProducto(int productoId) // Paso 21, agregado o servicio creado no proxecto Shared para dar mais datos das operacions que facemos 
        {
            var resposta = await _productoService.GetProductoAsync(productoId); //pasamos a peticion a traves do Servicio usando aqui _productoService
            return Ok(resposta); // e devolvemos o resultado
        }

        [HttpGet("categoria/{categoriaUrl}")]
        public async Task<ActionResult<ServiceResposta<List<Producto>>>> GetProductosPorCategoriaAsync(string categoriaUrl) // Paso 21, agregado o servicio creado no proxecto Shared para dar mais datos das operacions que facemos 
        {
            var resposta = await _productoService.GetProductosPorCategoriaAsync(categoriaUrl); //pasamos a peticion a traves do Servicio usando aqui _productoService
            return Ok(resposta); // e devolvemos o resultado
        }

        [HttpGet("search/{busquedaText}")]
        public async Task<ActionResult<ServiceResposta<List<Producto>>>> SearchProductos(string busquedaText)
        {
            var resposta = await _productoService.SearchProductos(busquedaText); //pasamos a peticion a traves do Servicio usando aqui _productoService
            return Ok(resposta); // e devolvemos o resultado
        }

        [HttpGet("searchsuggestions/{busquedaText}")]
        public async Task<ActionResult<ServiceResposta<List<Producto>>>> GetProductosSearchSuxerencias(string busquedaText)
        {
            var resposta = await _productoService.GetProductosSearchSuxerencias(busquedaText);
            return Ok(resposta);
        }

        [HttpGet("destacado")]
        public async Task<ActionResult<ServiceResposta<List<Producto>>>> GetProductosDestacados()
        {
            var resposta = await _productoService.GetProductosDestacados();
            return Ok(resposta);
        }

        // public async Task<ActionResult<List<Producto>>> GetProducto()
        // {
        //     var productos = await _context.Productos.ToListAsync();
        //     return Ok(productos);
        // }

        //a version de abaixo e igual so que non ofrece preview visual da nosa API en Swagger
        // [HttpGet]
        // public async Task<IActionResult> GetProducto()
        // {
        //     return Ok(Productos);
        // }
    }
}

//usado para data seeding
// private static List<Producto> Productos = new List<Producto>
// {
//     new Producto
//     {
//         Id = 1,
//         Titulo = "The Hitchhiker's Guide to the Galaxy",
//         Descripcion = "A Guía do Autoestopista Galáctico (título orixinal The Hitchhiker's Guide to the Galaxy) é unha serie de novelas humorísticas de ciencia ficción do escritor inglés Douglas Adams. Orixinalmente unha comedia radiofónica que comezou a emitirse na BBC en 1978, foi seguida dunha novela, The Hitchhiker's Guide to the Galaxy, publicada en 1979, unha serie de televisión en 1981, un xogo de ordenador en 1984 e unha película en 2005.",
//         ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
//         Precio = 9.99m
//     },
//     new Producto
//     {
//         Id = 2,
//         Titulo = "Ready Player One",
//         Descripcion = "Ready Player One é unha novela distópica de ciencia ficción de Ernest Cline publicada no ano 2011, a súa primeira obra deste tipo. No 2012 o libro recibiu un Premio Alex por parte da división Young Adult Library Services Association da American Library Association e gañou o Premio Prometheus de 2012.",
//         ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg",
//         Precio = 8.99m
//     },
//     new Producto
//     {
//         Id = 3,
//         Titulo = "Nineteen Eighty-Four",
//         Descripcion = "1984 (en inglés Nineteen Eighty-Four) é o título dunha novela de política ficción distópica escrita por George Orwell en 1948 e editada en 1949. Na novela o estado omnipresente obriga a cumprir as leis e normas aos membros do partido totalitario mediante o adoutrinamento, a propaganda, o medo e o castigo desapiadado. A novela introduciu os conceptos do sempre presente e vixiante Grande Irmán, do notorio cuarto 101, da ubicua policía do pensamento e da neolingua, adaptación do inglés na que se reduce e se transforma o léxico —o que non está na lingua, non pode ser pensado.",
//         ImaxeUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984first.jpg",
//         Precio = 11.99m
//     }
// };