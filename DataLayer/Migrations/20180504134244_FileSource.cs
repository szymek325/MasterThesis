using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class FileSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileSourceId",
                table: "File",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileSource",
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
                    table.PrimaryKey("PK_FileSource", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_FileSourceId",
                table: "File",
                column: "FileSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_File_FileSourceId",
                table: "File",
                column: "FileSourceId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_File_FileSourceId",
                table: "File");

            migrationBuilder.DropTable(
                name: "FileSource");

            migrationBuilder.DropIndex(
                name: "IX_File_FileSourceId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "FileSourceId",
                table: "File");
        }
    }
}
