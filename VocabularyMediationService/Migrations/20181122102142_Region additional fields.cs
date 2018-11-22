using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyMediationService.Migrations
{
    public partial class Regionadditionalfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Regions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BoundX1",
                table: "Regions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BoundX2",
                table: "Regions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BoundY1",
                table: "Regions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BoundY2",
                table: "Regions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WKT",
                table: "Regions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "BoundX1",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "BoundX2",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "BoundY1",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "BoundY2",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "WKT",
                table: "Regions");
        }
    }
}
