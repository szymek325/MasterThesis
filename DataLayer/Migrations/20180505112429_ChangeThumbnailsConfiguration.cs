using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class ChangeThumbnailsConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbFile",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Batch",
                table: "File",
                newName: "Thumbnail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "File",
                newName: "Batch");

            migrationBuilder.AddColumn<string>(
                name: "ThumbFile",
                table: "Person",
                nullable: true);
        }
    }
}
