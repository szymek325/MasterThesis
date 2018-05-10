using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class FaceRecoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetwork_Status_StatusId",
                table: "NeuralNetwork");

            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetworkPerson_NeuralNetwork_NeuralNetworkId",
                table: "NeuralNetworkPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NeuralNetwork",
                table: "NeuralNetwork");

            migrationBuilder.RenameTable(
                name: "NeuralNetwork",
                newName: "NeuralNetworks");

            migrationBuilder.RenameIndex(
                name: "IX_NeuralNetwork_StatusId",
                table: "NeuralNetworks",
                newName: "IX_NeuralNetworks_StatusId");

            migrationBuilder.AddColumn<string>(
                name: "FaceRecognitionId",
                table: "File",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NeuralNetworks",
                table: "NeuralNetworks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FaceRecognitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    FileId = table.Column<int>(nullable: true),
                    Guid = table.Column<string>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NeuralNetworkId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceRecognitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaceRecognitions_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaceRecognitions_NeuralNetworks_NeuralNetworkId",
                        column: x => x.NeuralNetworkId,
                        principalTable: "NeuralNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaceRecognitions_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaceRecognitions_FileId",
                table: "FaceRecognitions",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FaceRecognitions_NeuralNetworkId",
                table: "FaceRecognitions",
                column: "NeuralNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_FaceRecognitions_StatusId",
                table: "FaceRecognitions",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetworkPerson_NeuralNetworks_NeuralNetworkId",
                table: "NeuralNetworkPerson",
                column: "NeuralNetworkId",
                principalTable: "NeuralNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetworks_Status_StatusId",
                table: "NeuralNetworks",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetworkPerson_NeuralNetworks_NeuralNetworkId",
                table: "NeuralNetworkPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetworks_Status_StatusId",
                table: "NeuralNetworks");

            migrationBuilder.DropTable(
                name: "FaceRecognitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NeuralNetworks",
                table: "NeuralNetworks");

            migrationBuilder.DropColumn(
                name: "FaceRecognitionId",
                table: "File");

            migrationBuilder.RenameTable(
                name: "NeuralNetworks",
                newName: "NeuralNetwork");

            migrationBuilder.RenameIndex(
                name: "IX_NeuralNetworks_StatusId",
                table: "NeuralNetwork",
                newName: "IX_NeuralNetwork_StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NeuralNetwork",
                table: "NeuralNetwork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetwork_Status_StatusId",
                table: "NeuralNetwork",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetworkPerson_NeuralNetwork_NeuralNetworkId",
                table: "NeuralNetworkPerson",
                column: "NeuralNetworkId",
                principalTable: "NeuralNetwork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
