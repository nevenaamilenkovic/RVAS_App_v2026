using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RvasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodatiOdgovoriNaKomentare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentari_AspNetUsers_KorisnikId",
                table: "Komentari");

            migrationBuilder.AddColumn<int>(
                name: "RoditeljskiKomentarId",
                table: "Komentari",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_RoditeljskiKomentarId",
                table: "Komentari",
                column: "RoditeljskiKomentarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentari_AspNetUsers_KorisnikId",
                table: "Komentari",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentari_Komentari_RoditeljskiKomentarId",
                table: "Komentari",
                column: "RoditeljskiKomentarId",
                principalTable: "Komentari",
                principalColumn: "KomentarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentari_AspNetUsers_KorisnikId",
                table: "Komentari");

            migrationBuilder.DropForeignKey(
                name: "FK_Komentari_Komentari_RoditeljskiKomentarId",
                table: "Komentari");

            migrationBuilder.DropIndex(
                name: "IX_Komentari_RoditeljskiKomentarId",
                table: "Komentari");

            migrationBuilder.DropColumn(
                name: "RoditeljskiKomentarId",
                table: "Komentari");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentari_AspNetUsers_KorisnikId",
                table: "Komentari",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
