using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ProcessingTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingTime",
                table: "RecognitionResult",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingTime",
                table: "NeuralNetworkFile",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingTime",
                table: "NeuralNetworkFile",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingTime",
                table: "DetectionResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingTime",
                table: "RecognitionResult");

            migrationBuilder.DropColumn(
                name: "ProcessingTime",
                table: "NeuralNetworkFile");

            migrationBuilder.DropColumn(
                name: "TrainingTime",
                table: "NeuralNetworkFile");

            migrationBuilder.DropColumn(
                name: "ProcessingTime",
                table: "DetectionResult");
        }
    }
}
