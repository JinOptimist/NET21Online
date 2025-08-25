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
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeProducts_UserCoffeShops_AuthorAddId",
                table: "CoffeeProducts");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeProducts_AuthorAddId",
                table: "CoffeeProducts");

            migrationBuilder.DropColumn(
                name: "AuthorAddId",
                table: "CoffeeProducts");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "CoffeeProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CallRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeProducts_AuthorId",
                table: "CoffeeProducts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRequests_Id",
                table: "CallRequests",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeProducts_UserCoffeShops_AuthorId",
                table: "CoffeeProducts",
                column: "AuthorId",
                principalTable: "UserCoffeShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
