using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DataContext _context; //engadido con dependency injection
        public ProductoController(DataContext context)
        {
            _context = context;
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

        /// <summary>
        /// Esta version permite ver o Schema e o modelo da nosa API en Swagger
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProducto()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        //a version de abaixo e igual so que non ofrece preview visual da nosa API en Swagger
        // [HttpGet]
        // public async Task<IActionResult> GetProducto()
        // {
        //     return Ok(Productos);
        // }
    }
}
