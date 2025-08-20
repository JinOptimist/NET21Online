using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Girls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GirlUser",
                columns: table => new
                {
                    FavoriteGirlsId = table.Column<int>(type: "int", nullable: false),
                    UserWhoAddToFavoriteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GirlUser", x => new { x.FavoriteGirlsId, x.UserWhoAddToFavoriteId });
                    table.ForeignKey(
                        name: "FK_GirlUser_Girls_FavoriteGirlsId",
                        column: x => x.FavoriteGirlsId,
                        principalTable: "Girls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GirlUser_Users_UserWhoAddToFavoriteId",
                        column: x => x.UserWhoAddToFavoriteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Girls_AuthorId",
                table: "Girls",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_GirlUser_UserWhoAddToFavoriteId",
                table: "GirlUser",
                column: "UserWhoAddToFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Girls_Users_AuthorId",
                table: "Girls",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Girls_Users_AuthorId",
                table: "Girls");

            migrationBuilder.DropTable(
                name: "GirlUser");

            migrationBuilder.DropIndex(
                name: "IX_Girls_AuthorId",
                table: "Girls");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Girls");
        }
    }
}
