using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFieldsFromTourismTableAndReanameSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
  
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewAuthor",
                table: "TourismShops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TitleNameId",
                table: "Tourisms",
                type: "int",
                nullable: true);

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
        }
    }
}
