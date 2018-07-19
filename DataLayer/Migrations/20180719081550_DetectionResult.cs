using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class DetectionResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectionImage_Detection_DetectionId",
                table: "DetectionImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetworkFile_NeuralNetwork_NeuralNetworkId",
                table: "NeuralNetworkFile");

            migrationBuilder.DropForeignKey(
                name: "FK_RecognitionImage_Recognition_RecognitionId",
                table: "RecognitionImage");

            migrationBuilder.AlterColumn<int>(
                name: "RecognitionId",
                table: "RecognitionImage",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NeuralNetworkId",
                table: "NeuralNetworkFile",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DetectionId",
                table: "DetectionImage",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DetectionType",
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
                    table.PrimaryKey("PK_DetectionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetectionResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DetectionId = table.Column<int>(nullable: true),
                    DetectionTypeId = table.Column<int>(nullable: false),
                    EndX = table.Column<int>(nullable: false),
                    EndY = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    StartX = table.Column<int>(nullable: false),
                    StartY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetectionResult_Detection_DetectionId",
                        column: x => x.DetectionId,
                        principalTable: "Detection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetectionResult_DetectionType_DetectionTypeId",
                        column: x => x.DetectionTypeId,
                        principalTable: "DetectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetectionResultImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DetectionResultId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectionResultImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetectionResultImage_DetectionResult_DetectionResultId",
                        column: x => x.DetectionResultId,
                        principalTable: "DetectionResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetectionResult_DetectionId",
                table: "DetectionResult",
                column: "DetectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectionResult_DetectionTypeId",
                table: "DetectionResult",
                column: "DetectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectionResultImage_DetectionResultId",
                table: "DetectionResultImage",
                column: "DetectionResultId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetectionImage_Detection_DetectionId",
                table: "DetectionImage",
                column: "DetectionId",
                principalTable: "Detection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetworkFile_NeuralNetwork_NeuralNetworkId",
                table: "NeuralNetworkFile",
                column: "NeuralNetworkId",
                principalTable: "NeuralNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecognitionImage_Recognition_RecognitionId",
                table: "RecognitionImage",
                column: "RecognitionId",
                principalTable: "Recognition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectionImage_Detection_DetectionId",
                table: "DetectionImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetworkFile_NeuralNetwork_NeuralNetworkId",
                table: "NeuralNetworkFile");

            migrationBuilder.DropForeignKey(
                name: "FK_RecognitionImage_Recognition_RecognitionId",
                table: "RecognitionImage");

            migrationBuilder.DropTable(
                name: "DetectionResultImage");

            migrationBuilder.DropTable(
                name: "DetectionResult");

            migrationBuilder.DropTable(
                name: "DetectionType");

            migrationBuilder.AlterColumn<int>(
                name: "RecognitionId",
                table: "RecognitionImage",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "NeuralNetworkId",
                table: "NeuralNetworkFile",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DetectionId",
                table: "DetectionImage",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DetectionImage_Detection_DetectionId",
                table: "DetectionImage",
                column: "DetectionId",
                principalTable: "Detection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetworkFile_NeuralNetwork_NeuralNetworkId",
                table: "NeuralNetworkFile",
                column: "NeuralNetworkId",
                principalTable: "NeuralNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecognitionImage_Recognition_RecognitionId",
                table: "RecognitionImage",
                column: "RecognitionId",
                principalTable: "Recognition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
