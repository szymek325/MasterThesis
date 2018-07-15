using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class RecognitionResultRequestId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecognitionResult_Recognition_RecognitionId",
                table: "RecognitionResult");

            migrationBuilder.AlterColumn<int>(
                name: "RecognitionId",
                table: "RecognitionResult",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecognitionResult_Recognition_RecognitionId",
                table: "RecognitionResult",
                column: "RecognitionId",
                principalTable: "Recognition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecognitionResult_Recognition_RecognitionId",
                table: "RecognitionResult");

            migrationBuilder.AlterColumn<int>(
                name: "RecognitionId",
                table: "RecognitionResult",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_RecognitionResult_Recognition_RecognitionId",
                table: "RecognitionResult",
                column: "RecognitionId",
                principalTable: "Recognition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
