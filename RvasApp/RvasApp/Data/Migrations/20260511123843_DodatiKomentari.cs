using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RvasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodatiKomentari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komentari",
                columns: table => new
                {
                    KomentarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPostavljanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentari", x => x.KomentarId);
                    table.ForeignKey(
                        name: "FK_Komentari_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentari_Postovi_PostId",
                        column: x => x.PostId,
                        principalTable: "Postovi",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_KorisnikId",
                table: "Komentari",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_PostId",
                table: "Komentari",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentari");
        }
    }
}
