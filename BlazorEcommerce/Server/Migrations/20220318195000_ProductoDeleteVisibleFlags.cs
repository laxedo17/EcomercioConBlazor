using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class ProductoDeleteVisibleFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ProductoVariantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "ProductoVariantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 1, 2 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 1, 3 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 1, 4 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 2, 2 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 3, 2 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 4, 5 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 4, 6 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 4, 7 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 5, 5 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 6, 5 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 7, 8 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 7, 9 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 7, 10 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 8, 8 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 9, 8 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 10, 1 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductoVariantes",
                keyColumns: new[] { "ProductoId", "ProductoTypeId" },
                keyValues: new object[] { 11, 1 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 11,
                column: "Visible",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProductoVariantes");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "ProductoVariantes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Productos");
        }
    }
}
