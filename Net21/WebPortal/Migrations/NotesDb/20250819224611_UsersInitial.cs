using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebPortal.Migrations.NotesDb
{
    /// <inheritdoc />
    public partial class UsersInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Notes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false),
                    Money = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteUser",
                columns: table => new
                {
                    FavoriteNotesId = table.Column<int>(type: "integer", nullable: false),
                    UserWhoAddToFavoriteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteUser", x => new { x.FavoriteNotesId, x.UserWhoAddToFavoriteId });
                    table.ForeignKey(
                        name: "FK_NoteUser_Notes_FavoriteNotesId",
                        column: x => x.FavoriteNotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteUser_Users_UserWhoAddToFavoriteId",
                        column: x => x.UserWhoAddToFavoriteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AuthorId",
                table: "Notes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteUser_UserWhoAddToFavoriteId",
                table: "NoteUser",
                column: "UserWhoAddToFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_AuthorId",
                table: "Notes",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_AuthorId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "NoteUser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Notes_AuthorId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Notes");
        }
    }
}
