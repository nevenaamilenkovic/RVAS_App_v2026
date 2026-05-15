using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RvasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodateKategorije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Postovi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postovi_KategorijaId",
                table: "Postovi",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postovi_Kategorije_KategorijaId",
                table: "Postovi",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "KategorijaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postovi_Kategorije_KategorijaId",
                table: "Postovi");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropIndex(
                name: "IX_Postovi_KategorijaId",
                table: "Postovi");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Postovi");
        }
    }
}
