using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class FilesConnectionsFIx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceDetection_Status_FileId",
                table: "FaceDetection");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceDetection_File_FileId",
                table: "FaceDetection",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceDetection_File_FileId",
                table: "FaceDetection");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceDetection_Status_FileId",
                table: "FaceDetection",
                column: "FileId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
