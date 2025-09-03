using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoffeeProductAuthorToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_CoffeeProducts_UserCoffeShops_AuthorId",
               table: "CoffeeProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeProducts_Users_AuthorId", // Новое имя для ключа
                table: "CoffeeProducts",
                column: "AuthorId",
                principalTable: "Users", 
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Действие при удалении
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_CoffeeProducts_Users_AuthorId",
               table: "CoffeeProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeProducts_UserCoffeShops_AuthorId",
                table: "CoffeeProducts",
                column: "AuthorId",
                principalTable: "UserCoffeShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


        }
    }
}
