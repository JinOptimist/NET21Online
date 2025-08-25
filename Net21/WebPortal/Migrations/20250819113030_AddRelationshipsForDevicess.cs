using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipsForDevicess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Categoryes_CategoryId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_TypeDevices_TypeDeviceId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_TypeDeviceId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "TypeDeviceId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    Memory = table.Column<double>(type: "float", nullable: false),
                    VideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Motherboard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PowerUnit = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computers_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices",
                column: "CategoryId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TypeDeviceId",
                table: "Devices",
                column: "TypeDeviceId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_DeviceId",
                table: "Computers",
                column: "DeviceId",
                unique: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Categoryes_CategoryId",
                table: "Devices",
                column: "CategoryId",
                principalTable: "Categoryes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_TypeDevices_TypeDeviceId",
                table: "Devices",
                column: "TypeDeviceId",
                principalTable: "TypeDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Categoryes_CategoryId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_TypeDevices_TypeDeviceId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_TypeDeviceId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "TypeDeviceId",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TypeDeviceId",
                table: "Devices",
                column: "TypeDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Categoryes_CategoryId",
                table: "Devices",
                column: "CategoryId",
                principalTable: "Categoryes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_TypeDevices_TypeDeviceId",
                table: "Devices",
                column: "TypeDeviceId",
                principalTable: "TypeDevices",
                principalColumn: "Id");
        }
    }
}
