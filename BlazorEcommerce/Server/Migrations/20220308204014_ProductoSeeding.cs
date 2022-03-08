using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class ProductoSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Descripcion", "ImaxeUrl", "Precio", "Titulo" },
                values: new object[] { 1, "A Guía do Autoestopista Galáctico (título orixinal The Hitchhiker's Guide to the Galaxy) é unha serie de novelas humorísticas de ciencia ficción do escritor inglés Douglas Adams. Orixinalmente unha comedia radiofónica que comezou a emitirse na BBC en 1978, foi seguida dunha novela, The Hitchhiker's Guide to the Galaxy, publicada en 1979, unha serie de televisión en 1981, un xogo de ordenador en 1984 e unha película en 2005.", "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg", 9.99m, "The Hitchhiker's Guide to the Galaxy" });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Descripcion", "ImaxeUrl", "Precio", "Titulo" },
                values: new object[] { 2, "Ready Player One é unha novela distópica de ciencia ficción de Ernest Cline publicada no ano 2011, a súa primeira obra deste tipo. No 2012 o libro recibiu un Premio Alex por parte da división Young Adult Library Services Association da American Library Association e gañou o Premio Prometheus de 2012.", "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg", 8.99m, "Ready Player One" });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Descripcion", "ImaxeUrl", "Precio", "Titulo" },
                values: new object[] { 3, "1984 (en inglés Nineteen Eighty-Four) é o título dunha novela de política ficción distópica escrita por George Orwell en 1948 e editada en 1949. Na novela o estado omnipresente obriga a cumprir as leis e normas aos membros do partido totalitario mediante o adoutrinamento, a propaganda, o medo e o castigo desapiadado. A novela introduciu os conceptos do sempre presente e vixiante Grande Irmán, do notorio cuarto 101, da ubicua policía do pensamento e da neolingua, adaptación do inglés na que se reduce e se transforma o léxico —o que non está na lingua, non pode ser pensado.", "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984first.jpg", 11.99m, "Nineteen Eighty-Four" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
