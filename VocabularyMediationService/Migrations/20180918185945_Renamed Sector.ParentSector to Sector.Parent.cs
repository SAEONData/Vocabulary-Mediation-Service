using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VocabularyMediationService.Migrations
{
    public partial class RenamedSectorParentSectortoSectorParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sectors_Sectors_ParentSectorId",
                table: "Sectors");

            migrationBuilder.RenameColumn(
                name: "ParentSectorId",
                table: "Sectors",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Sectors_ParentSectorId",
                table: "Sectors",
                newName: "IX_Sectors_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sectors_Sectors_ParentId",
                table: "Sectors",
                column: "ParentId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sectors_Sectors_ParentId",
                table: "Sectors");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Sectors",
                newName: "ParentSectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Sectors_ParentId",
                table: "Sectors",
                newName: "IX_Sectors_ParentSectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sectors_Sectors_ParentSectorId",
                table: "Sectors",
                column: "ParentSectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
