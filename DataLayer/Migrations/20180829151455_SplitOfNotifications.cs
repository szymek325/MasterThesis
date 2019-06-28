using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class SplitOfNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageAttachment_Notification_NotificationId",
                table: "ImageAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_NotificationType_NotificationTypeId",
                table: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropIndex(
                name: "IX_Notification_NotificationTypeId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_ImageAttachment_NotificationId",
                table: "ImageAttachment");

            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "ImageAttachment",
                newName: "MovementId");

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ImageAttachmentType",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreationTime", "Name" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movement" });

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_MovementId",
                table: "ImageAttachment",
                column: "MovementId",
                unique: true,
                filter: "[MovementId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageAttachment_Movement_MovementId",
                table: "ImageAttachment",
                column: "MovementId",
                principalTable: "Movement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageAttachment_Movement_MovementId",
                table: "ImageAttachment");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_ImageAttachment_MovementId",
                table: "ImageAttachment");

            migrationBuilder.RenameColumn(
                name: "MovementId",
                table: "ImageAttachment",
                newName: "NotificationId");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "Notification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ImageAttachmentType",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreationTime", "Name" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motion" });

            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "CreationTime", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sensor Reading" });

            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "CreationTime", "ModifiedDate", "Name" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Motion Detection" });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTypeId",
                table: "Notification",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_NotificationId",
                table: "ImageAttachment",
                column: "NotificationId",
                unique: true,
                filter: "[NotificationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageAttachment_Notification_NotificationId",
                table: "ImageAttachment",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_NotificationType_NotificationTypeId",
                table: "Notification",
                column: "NotificationTypeId",
                principalTable: "NotificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
