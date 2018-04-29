using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class StatusIdMayBeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceDetection_Status_StatusId",
                table: "FaceDetection");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "FaceDetection",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FaceDetection_Status_StatusId",
                table: "FaceDetection",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceDetection_Status_StatusId",
                table: "FaceDetection");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "FaceDetection",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FaceDetection_Status_StatusId",
                table: "FaceDetection",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
