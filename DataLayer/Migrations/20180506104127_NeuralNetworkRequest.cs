using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class NeuralNetworkRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "File");

            migrationBuilder.CreateTable(
                name: "NeuralNetworkRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuralNetworkRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NeuralNetworkRequestPerson",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    NeuralNetworkRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuralNetworkRequestPerson", x => new { x.PersonId, x.NeuralNetworkRequestId });
                    table.ForeignKey(
                        name: "FK_NeuralNetworkRequestPerson_NeuralNetworkRequest_NeuralNetworkRequestId",
                        column: x => x.NeuralNetworkRequestId,
                        principalTable: "NeuralNetworkRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeuralNetworkRequestPerson_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NeuralNetworkRequestPerson_NeuralNetworkRequestId",
                table: "NeuralNetworkRequestPerson",
                column: "NeuralNetworkRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NeuralNetworkRequestPerson");

            migrationBuilder.DropTable(
                name: "NeuralNetworkRequest");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "File",
                nullable: true);
        }
    }
}
