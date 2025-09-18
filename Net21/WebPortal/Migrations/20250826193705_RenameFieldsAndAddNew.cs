using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class RenameFieldsAndAddNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourismShops_Users_AuthorNameId",
                table: "TourismShops");

            migrationBuilder.RenameColumn(
                name: "TourImg",
                table: "TourismShops",
                newName: "TourImgUrl");

            migrationBuilder.RenameColumn(
                name: "AuthorNameId",
                table: "TourismShops",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_TourismShops_AuthorNameId",
                table: "TourismShops",
                newName: "IX_TourismShops_AuthorId");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Tourisms",
                newName: "TourName");

            migrationBuilder.RenameColumn(
                name: "TitleRating",
                table: "Tourisms",
                newName: "TourRating");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tourisms",
                newName: "TourImgUrl");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "Tourisms",
                newName: "DaysToPrepareTour");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TourismShops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "TourismShops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TourismShops_Users_AuthorId",
                table: "TourismShops",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourismShops_Users_AuthorId",
                table: "TourismShops");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TourismShops");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TourismShops");

            migrationBuilder.RenameColumn(
                name: "TourImgUrl",
                table: "TourismShops",
                newName: "TourImg");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "TourismShops",
                newName: "AuthorNameId");

            migrationBuilder.RenameIndex(
                name: "IX_TourismShops_AuthorId",
                table: "TourismShops",
                newName: "IX_TourismShops_AuthorNameId");

            migrationBuilder.RenameColumn(
                name: "TourRating",
                table: "Tourisms",
                newName: "TitleRating");

            migrationBuilder.RenameColumn(
                name: "TourName",
                table: "Tourisms",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "TourImgUrl",
                table: "Tourisms",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DaysToPrepareTour",
                table: "Tourisms",
                newName: "Days");

            migrationBuilder.AddForeignKey(
                name: "FK_TourismShops_Users_AuthorNameId",
                table: "TourismShops",
                column: "AuthorNameId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
