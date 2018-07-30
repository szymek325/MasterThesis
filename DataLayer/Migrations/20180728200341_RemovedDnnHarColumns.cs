using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemovedDnnHarColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DnnFaces",
                table: "Detection");

            migrationBuilder.DropColumn(
                name: "HaarFaces",
                table: "Detection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DnnFaces",
                table: "Detection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaarFaces",
                table: "Detection",
                nullable: false,
                defaultValue: 0);
        }
    }
}
