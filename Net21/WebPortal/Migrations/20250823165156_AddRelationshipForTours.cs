using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipForTours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorIdId",
                table: "TourismShops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TitleNameId",
                table: "Tourisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TourismShops_AuthorIdId",
                table: "TourismShops",
                column: "AuthorIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Tourisms_TitleNameId",
                table: "Tourisms",
                column: "TitleNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tourisms_TourismShops_TitleNameId",
                table: "Tourisms",
                column: "TitleNameId",
                principalTable: "TourismShops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourismShops_Users_AuthorIdId",
                table: "TourismShops",
                column: "AuthorIdId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tourisms_TourismShops_TitleNameId",
                table: "Tourisms");

            migrationBuilder.DropForeignKey(
                name: "FK_TourismShops_Users_AuthorIdId",
                table: "TourismShops");

            migrationBuilder.DropIndex(
                name: "IX_TourismShops_AuthorIdId",
                table: "TourismShops");

            migrationBuilder.DropIndex(
                name: "IX_Tourisms_TitleNameId",
                table: "Tourisms");

            migrationBuilder.DropColumn(
                name: "AuthorIdId",
                table: "TourismShops");

            migrationBuilder.DropColumn(
                name: "TitleNameId",
                table: "Tourisms");
        }
    }
}
