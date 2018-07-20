using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ImageAttachmentType",
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
                    table.PrimaryKey("PK_ImageAttachmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NeuralNetworkType",
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
                    table.PrimaryKey("PK_NeuralNetworkType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
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
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorsReading",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Humidity = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Temperature = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorsReading", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
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
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DnnFaces = table.Column<int>(nullable: false),
                    HaarFaces = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detection_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NeuralNetwork",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuralNetwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NeuralNetwork_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetectionResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DetectionId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetectionResult_DetectionType_DetectionTypeId",
                        column: x => x.DetectionTypeId,
                        principalTable: "DetectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NeuralNetworkFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NeuralNetworkId = table.Column<int>(nullable: false),
                    NeuralNetworkTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuralNetworkFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NeuralNetworkFile_NeuralNetwork_NeuralNetworkId",
                        column: x => x.NeuralNetworkId,
                        principalTable: "NeuralNetwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeuralNetworkFile_NeuralNetworkType_NeuralNetworkTypeId",
                        column: x => x.NeuralNetworkTypeId,
                        principalTable: "NeuralNetworkType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NeuralNetworkPeople",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    NeuralNetworkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuralNetworkPeople", x => new { x.PersonId, x.NeuralNetworkId });
                    table.ForeignKey(
                        name: "FK_NeuralNetworkPeople_NeuralNetwork_NeuralNetworkId",
                        column: x => x.NeuralNetworkId,
                        principalTable: "NeuralNetwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeuralNetworkPeople_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recognition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NeuralNetworkId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recognition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recognition_NeuralNetwork_NeuralNetworkId",
                        column: x => x.NeuralNetworkId,
                        principalTable: "NeuralNetwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recognition_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DetectionId = table.Column<int>(nullable: true),
                    DetectionResultId = table.Column<int>(nullable: true),
                    ImageAttachmentTypeId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    RecognitionId = table.Column<int>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageAttachment_Detection_DetectionId",
                        column: x => x.DetectionId,
                        principalTable: "Detection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageAttachment_DetectionResult_DetectionResultId",
                        column: x => x.DetectionResultId,
                        principalTable: "DetectionResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageAttachment_ImageAttachmentType_ImageAttachmentTypeId",
                        column: x => x.ImageAttachmentTypeId,
                        principalTable: "ImageAttachmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageAttachment_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageAttachment_Recognition_RecognitionId",
                        column: x => x.RecognitionId,
                        principalTable: "Recognition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    RecognitionId = table.Column<int>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detection_StatusId",
                table: "Detection",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectionResult_DetectionId",
                table: "DetectionResult",
                column: "DetectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectionResult_DetectionTypeId",
                table: "DetectionResult",
                column: "DetectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_DetectionId",
                table: "ImageAttachment",
                column: "DetectionId",
                unique: true,
                filter: "[DetectionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_DetectionResultId",
                table: "ImageAttachment",
                column: "DetectionResultId",
                unique: true,
                filter: "[DetectionResultId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_ImageAttachmentTypeId",
                table: "ImageAttachment",
                column: "ImageAttachmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_PersonId",
                table: "ImageAttachment",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAttachment_RecognitionId",
                table: "ImageAttachment",
                column: "RecognitionId",
                unique: true,
                filter: "[RecognitionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NeuralNetwork_StatusId",
                table: "NeuralNetwork",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuralNetworkFile_NeuralNetworkId",
                table: "NeuralNetworkFile",
                column: "NeuralNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuralNetworkFile_NeuralNetworkTypeId",
                table: "NeuralNetworkFile",
                column: "NeuralNetworkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuralNetworkPeople_NeuralNetworkId",
                table: "NeuralNetworkPeople",
                column: "NeuralNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Recognition_NeuralNetworkId",
                table: "Recognition",
                column: "NeuralNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Recognition_StatusId",
                table: "Recognition",
                column: "StatusId");

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
                name: "ImageAttachment");

            migrationBuilder.DropTable(
                name: "NeuralNetworkPeople");

            migrationBuilder.DropTable(
                name: "RecognitionResult");

            migrationBuilder.DropTable(
                name: "SensorsReading");

            migrationBuilder.DropTable(
                name: "DetectionResult");

            migrationBuilder.DropTable(
                name: "ImageAttachmentType");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "NeuralNetworkFile");

            migrationBuilder.DropTable(
                name: "Recognition");

            migrationBuilder.DropTable(
                name: "Detection");

            migrationBuilder.DropTable(
                name: "DetectionType");

            migrationBuilder.DropTable(
                name: "NeuralNetworkType");

            migrationBuilder.DropTable(
                name: "NeuralNetwork");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
