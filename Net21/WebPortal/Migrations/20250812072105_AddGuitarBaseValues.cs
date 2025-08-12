using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddGuitarBaseValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Guitars",
                columns: new[] { "Id", "ImageUrl", "Mark", "Name", "Price", "ReviewAmount", "Status" },
                values: new object[,]
                {
                    { 1, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 2, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 3, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 2 },
                    { 4, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 5, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 6, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 2 },
                    { 7, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 8, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 9, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 2 },
                    { 10, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 11, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 12, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Guitars",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
