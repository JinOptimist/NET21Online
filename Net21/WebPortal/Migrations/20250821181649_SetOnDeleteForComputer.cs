using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class SetOnDeleteForComputer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Devices_DeviceId",
                table: "Computers");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Devices_DeviceId",
                table: "Computers",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Devices_DeviceId",
                table: "Computers");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Devices_DeviceId",
                table: "Computers",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }
    }
}
