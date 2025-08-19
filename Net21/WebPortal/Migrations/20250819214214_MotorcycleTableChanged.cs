using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class MotorcycleTableChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Motorcycles");

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleBrandId",
                table: "Motorcycles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Motorcycles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleBrandId",
                table: "Motorcycles",
                column: "MotorcycleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_TypeId",
                table: "Motorcycles",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_MotorcycleTypes_TypeId",
                table: "Motorcycles",
                column: "TypeId",
                principalTable: "MotorcycleTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_MotorcyleBrands_MotorcycleBrandId",
                table: "Motorcycles",
                column: "MotorcycleBrandId",
                principalTable: "MotorcyleBrands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_MotorcycleTypes_TypeId",
                table: "Motorcycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_MotorcyleBrands_MotorcycleBrandId",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_MotorcycleBrandId",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_TypeId",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "MotorcycleBrandId",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Motorcycles");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Motorcycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
