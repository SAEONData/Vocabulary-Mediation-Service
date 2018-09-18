using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VocabularyMediationService.Migrations
{
    public partial class AddedHazardTypeParentHazard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HazardTypeId",
                table: "Hazards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Hazards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HazardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hazards_HazardTypeId",
                table: "Hazards",
                column: "HazardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hazards_ParentId",
                table: "Hazards",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hazards_HazardTypes_HazardTypeId",
                table: "Hazards",
                column: "HazardTypeId",
                principalTable: "HazardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hazards_Hazards_ParentId",
                table: "Hazards",
                column: "ParentId",
                principalTable: "Hazards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hazards_HazardTypes_HazardTypeId",
                table: "Hazards");

            migrationBuilder.DropForeignKey(
                name: "FK_Hazards_Hazards_ParentId",
                table: "Hazards");

            migrationBuilder.DropTable(
                name: "HazardTypes");

            migrationBuilder.DropIndex(
                name: "IX_Hazards_HazardTypeId",
                table: "Hazards");

            migrationBuilder.DropIndex(
                name: "IX_Hazards_ParentId",
                table: "Hazards");

            migrationBuilder.DropColumn(
                name: "HazardTypeId",
                table: "Hazards");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Hazards");
        }
    }
}
