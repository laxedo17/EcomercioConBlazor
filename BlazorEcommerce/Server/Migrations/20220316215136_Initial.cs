using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarroItems",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ProductoTypeId = table.Column<int>(type: "int", nullable: false),
                    Cantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarroItems", x => new { x.ProductoId, x.ProductoTypeId, x.UsuarioId });
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PedidoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DataCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImaxeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Destacado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoProductos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ProductoTypeId = table.Column<int>(type: "int", nullable: false),
                    Cantidade = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProductos", x => new { x.PedidoId, x.ProductoId, x.ProductoTypeId });
                    table.ForeignKey(
                        name: "FK_PedidoProductos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProductos_ProductoTypes_ProductoTypeId",
                        column: x => x.ProductoTypeId,
                        principalTable: "ProductoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoVariantes",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ProductoTypeId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrixinalPrecio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVariantes", x => new { x.ProductoId, x.ProductoTypeId });
                    table.ForeignKey(
                        name: "FK_ProductoVariantes_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoVariantes_ProductoTypes_ProductoTypeId",
                        column: x => x.ProductoTypeId,
                        principalTable: "ProductoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome", "Url" },
                values: new object[,]
                {
                    { 1, "Libros", "libros" },
                    { 2, "Peliculas", "peliculas" },
                    { 3, "Video xogos", "video-xogos" }
                });

            migrationBuilder.InsertData(
                table: "ProductoTypes",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Default" },
                    { 2, "Papel" },
                    { 3, "E-Book" },
                    { 4, "Audiobook" },
                    { 5, "Stream" },
                    { 6, "Blu-ray" },
                    { 7, "VHS" },
                    { 8, "PC" },
                    { 9, "PlayStation" },
                    { 10, "Xbox" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "Destacado", "ImaxeUrl", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, "A Guía do Autoestopista Galáctico (título orixinal The Hitchhiker's Guide to the Galaxy) é unha serie de novelas humorísticas de ciencia ficción do escritor inglés Douglas Adams. Orixinalmente unha comedia radiofónica que comezou a emitirse na BBC en 1978, foi seguida dunha novela, The Hitchhiker's Guide to the Galaxy, publicada en 1979, unha serie de televisión en 1981, un xogo de ordenador en 1984 e unha película en 2005.", true, "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg", "The Hitchhiker's Guide to the Galaxy" },
                    { 2, 1, "Ready Player One é unha novela distópica de ciencia ficción de Ernest Cline publicada no ano 2011, a súa primeira obra deste tipo. No 2012 o libro recibiu un Premio Alex por parte da división Young Adult Library Services Association da American Library Association e gañou o Premio Prometheus de 2012.", false, "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg", "Ready Player One" },
                    { 3, 1, "1984 (en inglés Nineteen Eighty-Four) é o título dunha novela de política ficción distópica escrita por George Orwell en 1948 e editada en 1949. Na novela o estado omnipresente obriga a cumprir as leis e normas aos membros do partido totalitario mediante o adoutrinamento, a propaganda, o medo e o castigo desapiadado. A novela introduciu os conceptos do sempre presente e vixiante Grande Irmán, do notorio cuarto 101, da ubicua policía do pensamento e da neolingua, adaptación do inglés na que se reduce e se transforma o léxico —o que non está na lingua, non pode ser pensado.", false, "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984first.jpg", "Nineteen Eighty-Four" },
                    { 4, 2, "Matrix (en inglés: The Matrix) é un filme de ciencia ficción escrito e dirixido por Lana e Lilly Wachowski e protagonizado por Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss e Hugo Weaving. Foi estreado nos Estados Unidos o 31 de marzo de 1999 e foi o primeiro da triloxía de Matrix e dunha serie de videoxogos, curtas animadas e bandas deseñadas. O filme gañou 4 Premios Oscar incluíndo a mellor montaxe, mellor son, mellor edición de son e mellores efectos visuais.", false, "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg", "The Matrix" },
                    { 5, 2, "Back to the Future é un filme estadounidense dirixido por Robert Zemeckis que foi estreado en 1985. Escrito por Zemeckis e Bob Gale, producido por Steven Spielberg, e protagonizado por Michael J. Fox, Christopher Lloyd, Lea Thompson, Crispin Glover e Thomas F. Wilson.", true, "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg", "Back to the Future" },
                    { 6, 2, "Toy Story (en galego, Historia de xoguetes) é unha película de animación xerada por computadora de Pixar e Walt Disney Pictures. Estreouse nos Estados Unidos o 22 de novembro de 1995. Foi a primeira longametraxe totalmente animada por computadora e o primeiro proxecto importante de Pixar no cine.", false, "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg", "Toy Story" },
                    { 7, 3, "Half-Life 2 (HL2) é a secuela do videoxogo Half-Life, unha aventura de videoxogos en primeira persoa. Anteriormente, o videoxogo vendíase xunto con Counter-Strike: Source, a secuela de Counter-Strike co novo motor gráfico e o motor físico (Havok) implementados e os niveis totalmente redeseñados. Actualmente pódese adquirir a través do paquete The Orange Box, que inclúe o xogo e as súas respectivas secuelas (Episode One e Episode Two) e tamén inclúe Team Fortress 2 e Portal, tamén feitos por Valve. Foi publicado o 16 de novembro de 2004, aínda que xa se podía descargar semanas antes a través de Steam. Foi galardoado como o xogo da década no décimo aniversario dos premios VGA.", false, "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg", "Half-Life 2" },
                    { 8, 3, "Diablo II é un xogo de role-playing Hack-And-Slash Publicado por Blizzard Entertainment en 2000 para Microsoft Windows, Classic Mac OS e MacOS.", false, "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png", "Diablo II" },
                    { 9, 3, "Día do tentáculo, tamén coñecido como Maniac Mansion II: Day of the Tentacle, [2] [3] é un xogo de aventura gráfica de 1993 desenvolvido e publicado por LucasArts. É a secuela do xogo Maniac Game Mansion de 1987. A trama segue a Bernard Bernoulli e os seus amigos de Hoagie e Laverne, xa que intentan deter o malvado tentáculo púrpura, un tentáculo sentente, desembarkado, de asumir o mundo. O xogador controla o trío e resolve crebacabezas mentres usa o tempo de viaxe para explorar diferentes períodos de historia..", true, "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg", "Day of the Tentacle" },
                    { 10, 3, "A Xbox é unha consola de videojuegos domésticos e a primeira entrega da serie Xbox de consolas de videojuegos fabricados por Microsoft.", false, "https://upload.wikimedia.org/wikipedia/commons/4/43/Xbox-console.jpg", "Xbox" },
                    { 11, 3, "O Super Nintendo Entertainment System (SNES), tamén coñecido como Super Nes ou Super Nintendo, é unha consola de videojuegos domésticos de 16 bits desenvolvida por Nintendo que foi lanzada en 1990 en Xapón e Corea do Sur.", false, "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg", "Super Nintendo Entertainment System" }
                });

            migrationBuilder.InsertData(
                table: "ProductoVariantes",
                columns: new[] { "ProductoId", "ProductoTypeId", "OrixinalPrecio", "Precio" },
                values: new object[,]
                {
                    { 1, 2, 19.99m, 9.99m },
                    { 1, 3, 0m, 7.99m },
                    { 1, 4, 29.99m, 19.99m },
                    { 2, 2, 14.99m, 7.99m },
                    { 3, 2, 0m, 6.99m },
                    { 4, 5, 0m, 3.99m },
                    { 4, 6, 0m, 9.99m },
                    { 4, 7, 0m, 19.99m },
                    { 5, 5, 0m, 3.99m },
                    { 6, 5, 0m, 2.99m },
                    { 7, 8, 29.99m, 19.99m },
                    { 7, 9, 0m, 69.99m },
                    { 7, 10, 59.99m, 49.99m },
                    { 8, 8, 24.99m, 9.99m },
                    { 9, 8, 0m, 14.99m },
                    { 10, 1, 299m, 159.99m },
                    { 11, 1, 399m, 79.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProductos_ProductoId",
                table: "PedidoProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProductos_ProductoTypeId",
                table: "PedidoProductos",
                column: "ProductoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVariantes_ProductoTypeId",
                table: "ProductoVariantes",
                column: "ProductoTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarroItems");

            migrationBuilder.DropTable(
                name: "PedidoProductos");

            migrationBuilder.DropTable(
                name: "ProductoVariantes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "ProductoTypes");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
