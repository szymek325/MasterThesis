using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class OneTomnay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceDetection_File_FileId",
                table: "FaceDetection");

            migrationBuilder.DropIndex(
                name: "IX_FaceDetection_FileId",
                table: "FaceDetection");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FaceDetection");

            migrationBuilder.AddColumn<int>(
                name: "FaceDetectionId",
                table: "File",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileDetectionId",
                table: "File",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_FaceDetectionId",
                table: "File",
                column: "FaceDetectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_FaceDetection_FaceDetectionId",
                table: "File",
                column: "FaceDetectionId",
                principalTable: "FaceDetection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_FaceDetection_FaceDetectionId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_FaceDetectionId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "FaceDetectionId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "FileDetectionId",
                table: "File");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "FaceDetection",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaceDetection_FileId",
                table: "FaceDetection",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceDetection_File_FileId",
                table: "FaceDetection",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
