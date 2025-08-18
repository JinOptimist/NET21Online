using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AllForMarketplace_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ConnectionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasNoiseCancellation = table.Column<bool>(type: "bit", nullable: true),
                    Impedance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrequencyResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasMicrophone = table.Column<bool>(type: "bit", nullable: true),
                    BatteryLife = table.Column<int>(type: "int", nullable: true),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RAM = table.Column<int>(type: "int", nullable: true),
                    Storage = table.Column<int>(type: "int", nullable: true),
                    StorageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenSize = table.Column<double>(type: "float", nullable: true),
                    GraphicsCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasHeartRateMonitor = table.Column<bool>(type: "bit", nullable: true),
                    HasBloodOxygenMonitor = table.Column<bool>(type: "bit", nullable: true),
                    IsWaterResistant = table.Column<bool>(type: "bit", nullable: true),
                    WaterResistanceLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompatibleOS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmartWatch_BatteryLife = table.Column<int>(type: "int", nullable: true),
                    Smartphone_OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OSVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Smartphone_ScreenSize = table.Column<double>(type: "float", nullable: true),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatteryCapacity = table.Column<int>(type: "int", nullable: true),
                    Has5G = table.Column<bool>(type: "bit", nullable: true),
                    CameraCount = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
