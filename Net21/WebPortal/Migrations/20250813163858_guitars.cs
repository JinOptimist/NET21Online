using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class guitars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guitars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mark = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReviewAmount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guitars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Guitars",
                columns: new[] { "Id", "ImageUrl", "Mark", "Name", "Price", "ReviewAmount", "Status" },
                values: new object[,]
                {
                    { 1, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 2, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 3, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 3 },
                    { 4, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 5, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 6, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 3 },
                    { 7, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 8, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 9, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 3 },
                    { 10, "ibanez-grg121-card.webp", 4m, "Ibanez GRG121", 400m, 101, 0 },
                    { 11, "les-paul.webp", 5m, "Gibson", 10000m, 1, 1 },
                    { 12, "cort-x100-opbk-card.webp", 2m, "Cort X100 OBPK", 200m, 22, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guitars");
        }
    }
}
