using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class SeedMaisProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "ImaxeUrl", "Precio", "Titulo" },
                values: new object[,]
                {
                    { 4, 2, "Matrix (en inglés: The Matrix) é un filme de ciencia ficción escrito e dirixido por Lana e Lilly Wachowski e protagonizado por Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss e Hugo Weaving. Foi estreado nos Estados Unidos o 31 de marzo de 1999 e foi o primeiro da triloxía de Matrix e dunha serie de videoxogos, curtas animadas e bandas deseñadas. O filme gañou 4 Premios Oscar incluíndo a mellor montaxe, mellor son, mellor edición de son e mellores efectos visuais.", "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg", 4.99m, "The Matrix" },
                    { 5, 2, "Back to the Future é un filme estadounidense dirixido por Robert Zemeckis que foi estreado en 1985. Escrito por Zemeckis e Bob Gale, producido por Steven Spielberg, e protagonizado por Michael J. Fox, Christopher Lloyd, Lea Thompson, Crispin Glover e Thomas F. Wilson.", "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg", 3.99m, "Back to the Future" },
                    { 6, 2, "Toy Story (en galego, Historia de xoguetes) é unha película de animación xerada por computadora de Pixar e Walt Disney Pictures. Estreouse nos Estados Unidos o 22 de novembro de 1995. Foi a primeira longametraxe totalmente animada por computadora e o primeiro proxecto importante de Pixar no cine.", "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg", 2.99m, "Toy Story" },
                    { 7, 3, "Half-Life 2 (HL2) é a secuela do videoxogo Half-Life, unha aventura de videoxogos en primeira persoa. Anteriormente, o videoxogo vendíase xunto con Counter-Strike: Source, a secuela de Counter-Strike co novo motor gráfico e o motor físico (Havok) implementados e os niveis totalmente redeseñados. Actualmente pódese adquirir a través do paquete The Orange Box, que inclúe o xogo e as súas respectivas secuelas (Episode One e Episode Two) e tamén inclúe Team Fortress 2 e Portal, tamén feitos por Valve. Foi publicado o 16 de novembro de 2004, aínda que xa se podía descargar semanas antes a través de Steam. Foi galardoado como o xogo da década no décimo aniversario dos premios VGA.", "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg", 49.99m, "Half-Life 2" },
                    { 8, 3, "Diablo II é un xogo de role-playing Hack-And-Slash Publicado por Blizzard Entertainment en 2000 para Microsoft Windows, Classic Mac OS e MacOS.", "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png", 9.99m, "Diablo II" },
                    { 9, 3, "Día do tentáculo, tamén coñecido como Maniac Mansion II: Day of the Tentacle, [2] [3] é un xogo de aventura gráfica de 1993 desenvolvido e publicado por LucasArts. É a secuela do xogo Maniac Game Mansion de 1987. A trama segue a Bernard Bernoulli e os seus amigos de Hoagie e Laverne, xa que intentan deter o malvado tentáculo púrpura, un tentáculo sentente, desembarkado, de asumir o mundo. O xogador controla o trío e resolve crebacabezas mentres usa o tempo de viaxe para explorar diferentes períodos de historia..", "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg", 14.99m, "Day of the Tentacle" },
                    { 10, 3, "A Xbox é unha consola de videojuegos domésticos e a primeira entrega da serie Xbox de consolas de videojuegos fabricados por Microsoft.", "https://upload.wikimedia.org/wikipedia/commons/4/43/Xbox-console.jpg", 159.99m, "Xbox" },
                    { 11, 3, "O Super Nintendo Entertainment System (SNES), tamén coñecido como Super Nes ou Super Nintendo, é unha consola de videojuegos domésticos de 16 bits desenvolvida por Nintendo que foi lanzada en 1990 en Xapón e Corea do Sur.", "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg", 79.99m, "Super Nintendo Entertainment System" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
