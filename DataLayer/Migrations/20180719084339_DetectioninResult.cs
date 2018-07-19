using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class DetectioninResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectionResult_Detection_DetectionId",
                table: "DetectionResult");

            migrationBuilder.AlterColumn<int>(
                name: "DetectionId",
                table: "DetectionResult",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetectionResult_Detection_DetectionId",
                table: "DetectionResult",
                column: "DetectionId",
                principalTable: "Detection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectionResult_Detection_DetectionId",
                table: "DetectionResult");

            migrationBuilder.AlterColumn<int>(
                name: "DetectionId",
                table: "DetectionResult",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DetectionResult_Detection_DetectionId",
                table: "DetectionResult",
                column: "DetectionId",
                principalTable: "Detection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
