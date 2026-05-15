using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RvasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodatiPostGlasovi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostGlasovi",
                columns: table => new
                {
                    PostVoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsUpvote = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGlasovi", x => x.PostVoteId);
                    table.ForeignKey(
                        name: "FK_PostGlasovi_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostGlasovi_Postovi_PostId",
                        column: x => x.PostId,
                        principalTable: "Postovi",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostGlasovi_KorisnikId",
                table: "PostGlasovi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGlasovi_PostId_KorisnikId",
                table: "PostGlasovi",
                columns: new[] { "PostId", "KorisnikId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostGlasovi");
        }
    }
}
