using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class CdekProjectUpdateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "CallRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CallRequestId",
                table: "CallRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "CallRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CallRequests_AuthorId",
                table: "CallRequests",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRequests_CallRequestId",
                table: "CallRequests",
                column: "CallRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CallRequests_CallRequests_CallRequestId",
                table: "CallRequests",
                column: "CallRequestId",
                principalTable: "CallRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CallRequests_Users_AuthorId",
                table: "CallRequests",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallRequests_CallRequests_CallRequestId",
                table: "CallRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CallRequests_Users_AuthorId",
                table: "CallRequests");

            migrationBuilder.DropIndex(
                name: "IX_CallRequests_AuthorId",
                table: "CallRequests");

            migrationBuilder.DropIndex(
                name: "IX_CallRequests_CallRequestId",
                table: "CallRequests");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CallRequests");

            migrationBuilder.DropColumn(
                name: "CallRequestId",
                table: "CallRequests");

            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "CallRequests");
        }
    }
}
