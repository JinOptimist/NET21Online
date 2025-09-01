using Microsoft.EntityFrameworkCore.Migrations;
using WebPortal.Enum;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: Language.Russian);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");
        }
    }
}
