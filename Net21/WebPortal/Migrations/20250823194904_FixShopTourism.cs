using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class FixShopTourism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourismShops_Users_AuthorIdId",
                table: "TourismShops");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "TourismShops");

            migrationBuilder.RenameColumn(
                name: "AuthorIdId",
                table: "TourismShops",
                newName: "AuthorNameId");

            migrationBuilder.RenameIndex(
                name: "IX_TourismShops_AuthorIdId",
                table: "TourismShops",
                newName: "IX_TourismShops_AuthorNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourismShops_Users_AuthorNameId",
                table: "TourismShops",
                column: "AuthorNameId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourismShops_Users_AuthorNameId",
                table: "TourismShops");

            migrationBuilder.RenameColumn(
                name: "AuthorNameId",
                table: "TourismShops",
                newName: "AuthorIdId");

            migrationBuilder.RenameIndex(
                name: "IX_TourismShops_AuthorNameId",
                table: "TourismShops",
                newName: "IX_TourismShops_AuthorIdId");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "TourismShops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_TourismShops_Users_AuthorIdId",
                table: "TourismShops",
                column: "AuthorIdId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
