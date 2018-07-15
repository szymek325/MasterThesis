using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class RecognitionResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecognitionResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Confidence = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    IdentifiedPersonId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    NeuralNetworkFileId = table.Column<int>(nullable: false),
                    RecognitionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecognitionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecognitionResult_NeuralNetworkFile_NeuralNetworkFileId",
                        column: x => x.NeuralNetworkFileId,
                        principalTable: "NeuralNetworkFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecognitionResult_Recognition_RecognitionId",
                        column: x => x.RecognitionId,
                        principalTable: "Recognition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecognitionResult_NeuralNetworkFileId",
                table: "RecognitionResult",
                column: "NeuralNetworkFileId");

            migrationBuilder.CreateIndex(
                name: "IX_RecognitionResult_RecognitionId",
                table: "RecognitionResult",
                column: "RecognitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecognitionResult");
        }
    }
}
