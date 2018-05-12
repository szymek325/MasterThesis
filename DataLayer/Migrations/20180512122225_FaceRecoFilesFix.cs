using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class FaceRecoFilesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "FaceRecognitionId",
                table: "File",
                newName: "FaceRecognitionGuid");

            migrationBuilder.AlterColumn<string>(
                name: "FaceRecognitionGuid",
                table: "File",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Guid",
                table: "FaceRecognitions",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FaceRecognitions_Guid",
                table: "FaceRecognitions",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_File_FaceRecognitionGuid",
                table: "File",
                column: "FaceRecognitionGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_File_FaceRecognitions_FaceRecognitionGuid",
                table: "File",
                column: "FaceRecognitionGuid",
                principalTable: "FaceRecognitions",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_FaceRecognitions_FaceRecognitionGuid",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_FaceRecognitionGuid",
                table: "File");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FaceRecognitions_Guid",
                table: "FaceRecognitions");

            migrationBuilder.RenameColumn(
                name: "FaceRecognitionGuid",
                table: "File",
                newName: "FaceRecognitionId");

            migrationBuilder.AlterColumn<string>(
                name: "FaceRecognitionId",
                table: "File",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FaceRecognitionId1",
                table: "File",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Guid",
                table: "FaceRecognitions",
                nullable: false,
                oldClrType: typeof(string));

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
    }
}
