using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class ChangesAfterMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourismShops_Users_AuthorId",
                table: "TourismShops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourismShops",
                table: "TourismShops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tourisms",
                table: "Tourisms");

            migrationBuilder.RenameTable(
                name: "TourismShops",
                newName: "Tours");

            migrationBuilder.RenameTable(
                name: "Tourisms",
                newName: "TourPreviews");

            migrationBuilder.RenameIndex(
                name: "IX_TourismShops_AuthorId",
                table: "Tours",
                newName: "IX_Tours_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourPreviews",
                table: "TourPreviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Users_AuthorId",
                table: "Tours",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Users_AuthorId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourPreviews",
                table: "TourPreviews");

            migrationBuilder.RenameTable(
                name: "Tours",
                newName: "TourismShops");

            migrationBuilder.RenameTable(
                name: "TourPreviews",
                newName: "Tourisms");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_AuthorId",
                table: "TourismShops",
                newName: "IX_TourismShops_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourismShops",
                table: "TourismShops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tourisms",
                table: "Tourisms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourismShops_Users_AuthorId",
                table: "TourismShops",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
