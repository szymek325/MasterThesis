using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class StatusInNN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "NeuralNetwork",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NeuralNetwork_StatusId",
                table: "NeuralNetwork",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_NeuralNetwork_Status_StatusId",
                table: "NeuralNetwork",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NeuralNetwork_Status_StatusId",
                table: "NeuralNetwork");

            migrationBuilder.DropIndex(
                name: "IX_NeuralNetwork_StatusId",
                table: "NeuralNetwork");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "NeuralNetwork");
        }
    }
}
