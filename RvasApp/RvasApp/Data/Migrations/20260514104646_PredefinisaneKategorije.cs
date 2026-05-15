using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RvasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class PredefinisaneKategorije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kategorije",
                columns: new[] { "KategorijaId", "Naziv" },
                values: new object[,]
                {
                    { 1, "Pitanja" },
                    { 2, "Saveti" },
                    { 3, "IT" },
                    { 4, "Novosti" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "KategorijaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "KategorijaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "KategorijaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "KategorijaId",
                keyValue: 4);
        }
    }
}
