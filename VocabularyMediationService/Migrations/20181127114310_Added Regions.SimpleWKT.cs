using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyMediationService.Migrations
{
    public partial class AddedRegionsSimpleWKT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SimpleWKT",
                table: "Regions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimpleWKT",
                table: "Regions");
        }
    }
}
