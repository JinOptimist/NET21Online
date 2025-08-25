using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddReleantionshipUserCoffeShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "CoffeeProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeProducts_UserCoffeShops_AuthorId",
                table: "CoffeeProducts");

            migrationBuilder.DropTable(
                name: "CallRequests");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeProducts_AuthorId",
                table: "CoffeeProducts");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "CoffeeProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AuthorAddId",
                table: "CoffeeProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeProducts_AuthorAddId",
                table: "CoffeeProducts",
                column: "AuthorAddId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeProducts_UserCoffeShops_AuthorAddId",
                table: "CoffeeProducts",
                column: "AuthorAddId",
                principalTable: "UserCoffeShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
