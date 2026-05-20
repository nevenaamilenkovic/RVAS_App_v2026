using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RvasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodatiKomentarGlasovi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KomentarGlasovi",
                columns: table => new
                {
                    KomentarVoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KomentarId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsUpvote = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentarGlasovi", x => x.KomentarVoteId);
                    table.ForeignKey(
                        name: "FK_KomentarGlasovi_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KomentarGlasovi_Komentari_KomentarId",
                        column: x => x.KomentarId,
                        principalTable: "Komentari",
                        principalColumn: "KomentarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomentarGlasovi_KomentarId_KorisnikId",
                table: "KomentarGlasovi",
                columns: new[] { "KomentarId", "KorisnikId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KomentarGlasovi_KorisnikId",
                table: "KomentarGlasovi",
                column: "KorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarGlasovi");
        }
    }
}
