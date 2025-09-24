using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations
{
    /// <inheritdoc />
    public partial class CdekProjectUpdateCdekChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CdekChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CdekChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CdekChat_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CdekChatUser",
                columns: table => new
                {
                    UserWhoViewedItId = table.Column<int>(type: "int", nullable: false),
                    ViewedChatMessagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CdekChatUser", x => new { x.UserWhoViewedItId, x.ViewedChatMessagesId });
                    table.ForeignKey(
                        name: "FK_CdekChatUser_CdekChat_ViewedChatMessagesId",
                        column: x => x.ViewedChatMessagesId,
                        principalTable: "CdekChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CdekChatUser_Users_UserWhoViewedItId",
                        column: x => x.UserWhoViewedItId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CdekChat_AuthorId",
                table: "CdekChat",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CdekChatUser_ViewedChatMessagesId",
                table: "CdekChatUser",
                column: "ViewedChatMessagesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CdekChatUser");

            migrationBuilder.DropTable(
                name: "CdekChat");
        }
    }
}
