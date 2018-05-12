using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class ImageToImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceRecognitions_File_FileId",
                table: "FaceRecognitions");

            migrationBuilder.DropIndex(
                name: "IX_FaceRecognitions_FileId",
                table: "FaceRecognitions");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FaceRecognitions");

            migrationBuilder.AddColumn<int>(
                name: "FaceRecognitionId1",
                table: "File",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_FaceRecognitionId1",
                table: "File",
                column: "FaceRecognitionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_File_FaceRecognitions_FaceRecognitionId1",
                table: "File",
                column: "FaceRecognitionId1",
                principalTable: "FaceRecognitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_FaceRecognitions_FaceRecognitionId1",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_FaceRecognitionId1",
                table: "File");

            migrationBuilder.DropColumn(
                name: "FaceRecognitionId1",
                table: "File");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "FaceRecognitions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaceRecognitions_FileId",
                table: "FaceRecognitions",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceRecognitions_File_FileId",
                table: "FaceRecognitions",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
