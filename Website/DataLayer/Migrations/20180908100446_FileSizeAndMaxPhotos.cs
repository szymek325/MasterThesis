using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FileSizeAndMaxPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileSize",
                table: "NeuralNetworkFile",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxNumberOfPhotosPerPerson",
                table: "NeuralNetwork",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "NeuralNetworkFile");

            migrationBuilder.DropColumn(
                name: "MaxNumberOfPhotosPerPerson",
                table: "NeuralNetwork");
        }
    }
}
