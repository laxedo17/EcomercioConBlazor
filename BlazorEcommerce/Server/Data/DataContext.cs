namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {
        //Modelos para a base de datos
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ProductoType> ProductoTypes { get; set; }
        public DbSet<ProductoVariante> ProductoVariantes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CarroItem> CarroItems { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProducto> PedidoProductos { get; set; }
        public DbSet<Direccion> Direccions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /// <summary>
        /// Para facer seed de datos na base de datos para taboas especificas, e facer unha migracion despois da inicial para ter algun dato.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //para a clave composta de id de producto e a id de tipo de producto (ProductoType)
            modelBuilder.Entity<ProductoVariante>()
                .HasKey(p => new { p.ProductoId, p.ProductoTypeId });

            //tamen necesitamos unha clave composta para os items de carro, a clave composta de id de producto e a id de tipo de producto e a Id de Usuario
            modelBuilder.Entity<CarroItem>()
                .HasKey(ci => new { ci.ProductoId, ci.ProductoTypeId, ci.UsuarioId });

            modelBuilder.Entity<PedidoProducto>()
            .HasKey(pepr => new { pepr.PedidoId, pepr.ProductoId, pepr.ProductoTypeId });

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    Id = 1,
                    Nome = "Libros",
                    Url = "libros"
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Peliculas",
                    Url = "peliculas"
                },
                new Categoria
                {
                    Id = 3,
                    Nome = "Video xogos",
                    Url = "video-xogos"
                }
            );

            modelBuilder.Entity<ProductoType>().HasData(
                    new ProductoType { Id = 1, Nome = "Default" },
                    new ProductoType { Id = 2, Nome = "Papel" },
                    new ProductoType { Id = 3, Nome = "E-Book" },
                    new ProductoType { Id = 4, Nome = "Audiobook" },
                    new ProductoType { Id = 5, Nome = "Stream" },
                    new ProductoType { Id = 6, Nome = "Blu-ray" },
                    new ProductoType { Id = 7, Nome = "VHS" },
                    new ProductoType { Id = 8, Nome = "PC" },
                    new ProductoType { Id = 9, Nome = "PlayStation" },
                    new ProductoType { Id = 10, Nome = "Xbox" }
                );

            modelBuilder.Entity<Producto>().HasData(
                new Producto
                {
                    Id = 1,
                    CategoriaId = 1,
                    Titulo = "The Hitchhiker's Guide to the Galaxy",
                    Descripcion = "A Guía do Autoestopista Galáctico (título orixinal The Hitchhiker's Guide to the Galaxy) é unha serie de novelas humorísticas de ciencia ficción do escritor inglés Douglas Adams. Orixinalmente unha comedia radiofónica que comezou a emitirse na BBC en 1978, foi seguida dunha novela, The Hitchhiker's Guide to the Galaxy, publicada en 1979, unha serie de televisión en 1981, un xogo de ordenador en 1984 e unha película en 2005.",
                    ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
                    Destacado = true
                },
                new Producto
                {
                    Id = 2,
                    CategoriaId = 1,
                    Titulo = "Ready Player One",
                    Descripcion = "Ready Player One é unha novela distópica de ciencia ficción de Ernest Cline publicada no ano 2011, a súa primeira obra deste tipo. No 2012 o libro recibiu un Premio Alex por parte da división Young Adult Library Services Association da American Library Association e gañou o Premio Prometheus de 2012.",
                    ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg"
                },
                new Producto
                {
                    Id = 3,
                    CategoriaId = 1,
                    Titulo = "Nineteen Eighty-Four",
                    Descripcion = "1984 (en inglés Nineteen Eighty-Four) é o título dunha novela de política ficción distópica escrita por George Orwell en 1948 e editada en 1949. Na novela o estado omnipresente obriga a cumprir as leis e normas aos membros do partido totalitario mediante o adoutrinamento, a propaganda, o medo e o castigo desapiadado. A novela introduciu os conceptos do sempre presente e vixiante Grande Irmán, do notorio cuarto 101, da ubicua policía do pensamento e da neolingua, adaptación do inglés na que se reduce e se transforma o léxico —o que non está na lingua, non pode ser pensado.",
                    ImaxeUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984first.jpg"
                },
                new Producto
                {
                    Id = 4,
                    CategoriaId = 2,
                    Titulo = "The Matrix",
                    Descripcion = "Matrix (en inglés: The Matrix) é un filme de ciencia ficción escrito e dirixido por Lana e Lilly Wachowski e protagonizado por Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss e Hugo Weaving. Foi estreado nos Estados Unidos o 31 de marzo de 1999 e foi o primeiro da triloxía de Matrix e dunha serie de videoxogos, curtas animadas e bandas deseñadas. O filme gañou 4 Premios Oscar incluíndo a mellor montaxe, mellor son, mellor edición de son e mellores efectos visuais.",
                    ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg"
                },
                    new Producto
                    {
                        Id = 5,
                        CategoriaId = 2,
                        Titulo = "Back to the Future",
                        Descripcion = "Back to the Future é un filme estadounidense dirixido por Robert Zemeckis que foi estreado en 1985. Escrito por Zemeckis e Bob Gale, producido por Steven Spielberg, e protagonizado por Michael J. Fox, Christopher Lloyd, Lea Thompson, Crispin Glover e Thomas F. Wilson.",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg",
                        Destacado = true
                    },
                    new Producto
                    {
                        Id = 6,
                        CategoriaId = 2,
                        Titulo = "Toy Story",
                        Descripcion = "Toy Story (en galego, Historia de xoguetes) é unha película de animación xerada por computadora de Pixar e Walt Disney Pictures. Estreouse nos Estados Unidos o 22 de novembro de 1995. Foi a primeira longametraxe totalmente animada por computadora e o primeiro proxecto importante de Pixar no cine.",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg"
                    },
                    new Producto
                    {
                        Id = 7,
                        CategoriaId = 3,
                        Titulo = "Half-Life 2",
                        Descripcion = "Half-Life 2 (HL2) é a secuela do videoxogo Half-Life, unha aventura de videoxogos en primeira persoa. Anteriormente, o videoxogo vendíase xunto con Counter-Strike: Source, a secuela de Counter-Strike co novo motor gráfico e o motor físico (Havok) implementados e os niveis totalmente redeseñados. Actualmente pódese adquirir a través do paquete The Orange Box, que inclúe o xogo e as súas respectivas secuelas (Episode One e Episode Two) e tamén inclúe Team Fortress 2 e Portal, tamén feitos por Valve. Foi publicado o 16 de novembro de 2004, aínda que xa se podía descargar semanas antes a través de Steam. Foi galardoado como o xogo da década no décimo aniversario dos premios VGA.",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg",
                    },
                    new Producto
                    {
                        Id = 8,
                        CategoriaId = 3,
                        Titulo = "Diablo II",
                        Descripcion = "Diablo II é un xogo de role-playing Hack-And-Slash Publicado por Blizzard Entertainment en 2000 para Microsoft Windows, Classic Mac OS e MacOS.",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png"
                    },
                    new Producto
                    {
                        Id = 9,
                        CategoriaId = 3,
                        Titulo = "Day of the Tentacle",
                        Descripcion = "Día do tentáculo, tamén coñecido como Maniac Mansion II: Day of the Tentacle, [2] [3] é un xogo de aventura gráfica de 1993 desenvolvido e publicado por LucasArts. É a secuela do xogo Maniac Game Mansion de 1987. A trama segue a Bernard Bernoulli e os seus amigos de Hoagie e Laverne, xa que intentan deter o malvado tentáculo púrpura, un tentáculo sentente, desembarkado, de asumir o mundo. O xogador controla o trío e resolve crebacabezas mentres usa o tempo de viaxe para explorar diferentes períodos de historia..",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg",
                        Destacado = true
                    },
                    new Producto
                    {
                        Id = 10,
                        CategoriaId = 3,
                        Titulo = "Xbox",
                        Descripcion = "A Xbox é unha consola de videojuegos domésticos e a primeira entrega da serie Xbox de consolas de videojuegos fabricados por Microsoft.",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/commons/4/43/Xbox-console.jpg"
                    },
                    new Producto
                    {
                        Id = 11,
                        CategoriaId = 3,
                        Titulo = "Super Nintendo Entertainment System",
                        Descripcion = "O Super Nintendo Entertainment System (SNES), tamén coñecido como Super Nes ou Super Nintendo, é unha consola de videojuegos domésticos de 16 bits desenvolvida por Nintendo que foi lanzada en 1990 en Xapón e Corea do Sur.",
                        ImaxeUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg"
                    }
            );

            modelBuilder.Entity<ProductoVariante>().HasData(
                new ProductoVariante
                {
                    ProductoId = 1,
                    ProductoTypeId = 2,
                    Precio = 9.99m,
                    OrixinalPrecio = 19.99m
                },
                new ProductoVariante
                {
                    ProductoId = 1,
                    ProductoTypeId = 3,
                    Precio = 7.99m
                },
                new ProductoVariante
                {
                    ProductoId = 1,
                    ProductoTypeId = 4,
                    Precio = 19.99m,
                    OrixinalPrecio = 29.99m
                },
                new ProductoVariante
                {
                    ProductoId = 2,
                    ProductoTypeId = 2,
                    Precio = 7.99m,
                    OrixinalPrecio = 14.99m
                },
                new ProductoVariante
                {
                    ProductoId = 3,
                    ProductoTypeId = 2,
                    Precio = 6.99m
                },
                new ProductoVariante
                {
                    ProductoId = 4,
                    ProductoTypeId = 5,
                    Precio = 3.99m
                },
                new ProductoVariante
                {
                    ProductoId = 4,
                    ProductoTypeId = 6,
                    Precio = 9.99m
                },
                new ProductoVariante
                {
                    ProductoId = 4,
                    ProductoTypeId = 7,
                    Precio = 19.99m
                },
                new ProductoVariante
                {
                    ProductoId = 5,
                    ProductoTypeId = 5,
                    Precio = 3.99m,
                },
                new ProductoVariante
                {
                    ProductoId = 6,
                    ProductoTypeId = 5,
                    Precio = 2.99m
                },
                new ProductoVariante
                {
                    ProductoId = 7,
                    ProductoTypeId = 8,
                    Precio = 19.99m,
                    OrixinalPrecio = 29.99m
                },
                new ProductoVariante
                {
                    ProductoId = 7,
                    ProductoTypeId = 9,
                    Precio = 69.99m
                },
                new ProductoVariante
                {
                    ProductoId = 7,
                    ProductoTypeId = 10,
                    Precio = 49.99m,
                    OrixinalPrecio = 59.99m
                },
                new ProductoVariante
                {
                    ProductoId = 8,
                    ProductoTypeId = 8,
                    Precio = 9.99m,
                    OrixinalPrecio = 24.99m,
                },
                new ProductoVariante
                {
                    ProductoId = 9,
                    ProductoTypeId = 8,
                    Precio = 14.99m
                },
                new ProductoVariante
                {
                    ProductoId = 10,
                    ProductoTypeId = 1,
                    Precio = 159.99m,
                    OrixinalPrecio = 299m
                },
                new ProductoVariante
                {
                    ProductoId = 11,
                    ProductoTypeId = 1,
                    Precio = 79.99m,
                    OrixinalPrecio = 399m
                }
            );
        }
    }
}
