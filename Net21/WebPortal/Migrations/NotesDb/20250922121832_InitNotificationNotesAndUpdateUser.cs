using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebPortal.Migrations.NotesDb
{
    /// <inheritdoc />
    public partial class InitNotificationNotesAndUpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationNotesUser",
                columns: table => new
                {
                    UserWhoViewedItId = table.Column<int>(type: "integer", nullable: false),
                    ViewedNotificationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationNotesUser", x => new { x.UserWhoViewedItId, x.ViewedNotificationId });
                    table.ForeignKey(
                        name: "FK_NotificationNotesUser_Notifications_ViewedNotificationId",
                        column: x => x.ViewedNotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationNotesUser_Users_UserWhoViewedItId",
                        column: x => x.UserWhoViewedItId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationNotesUser_ViewedNotificationId",
                table: "NotificationNotesUser",
                column: "ViewedNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AuthorId",
                table: "Notifications",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationNotesUser");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
