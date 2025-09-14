using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class LinkAnimeAndGirls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeGirl",
                columns: table => new
                {
                    AnimesId = table.Column<int>(type: "int", nullable: false),
                    CharactersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeGirl", x => new { x.AnimesId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_AnimeGirl_Animes_AnimesId",
                        column: x => x.AnimesId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeGirl_Girls_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Girls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGirl_CharactersId",
                table: "AnimeGirl",
                column: "CharactersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeGirl");
        }
    }
}
