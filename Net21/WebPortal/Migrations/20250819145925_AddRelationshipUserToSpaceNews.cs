using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipUserToSpaceNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "SpaceNews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpaceNews_AuthorId",
                table: "SpaceNews",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceNews_Users_AuthorId",
                table: "SpaceNews",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceNews_Users_AuthorId",
                table: "SpaceNews");

            migrationBuilder.DropIndex(
                name: "IX_SpaceNews_AuthorId",
                table: "SpaceNews");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "SpaceNews");
        }
    }
}
