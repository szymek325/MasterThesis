using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class FilesConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "FaceDetection",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaceDetection_FileId",
                table: "FaceDetection",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceDetection_Status_FileId",
                table: "FaceDetection",
                column: "FileId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceDetection_Status_FileId",
                table: "FaceDetection");

            migrationBuilder.DropIndex(
                name: "IX_FaceDetection_FileId",
                table: "FaceDetection");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FaceDetection");
        }
    }
}
