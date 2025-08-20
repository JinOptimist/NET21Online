using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCoffeShopRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "CoffeeProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCoffeShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCoffeShops", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeProducts_AuthorId",
                table: "CoffeeProducts",
                column: "AuthorId");

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
                name: "UserCoffeShops");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeProducts_AuthorId",
                table: "CoffeeProducts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CoffeeProducts");
        }
    }
}
