using Microsoft.EntityFrameworkCore.Migrations;

namespace Cs.Persistence.Migrations
{
    public partial class resources_add_coef_and_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coefficient",
                schema: "technic",
                table: "Technics",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "technic",
                table: "Technics",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "MainMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "material",
                table: "MainMaterials",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "ConsumptionMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "material",
                table: "ConsumptionMaterials",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "BuildingMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "material",
                table: "BuildingMaterials",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coefficient",
                schema: "hr",
                table: "Workers",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "hr",
                table: "Workers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coefficient",
                schema: "technic",
                table: "Technics");

            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "technic",
                table: "Technics");

            migrationBuilder.DropColumn(
                name: "Coefficient",
                schema: "material",
                table: "MainMaterials");

            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "material",
                table: "MainMaterials");

            migrationBuilder.DropColumn(
                name: "Coefficient",
                schema: "material",
                table: "ConsumptionMaterials");

            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "material",
                table: "ConsumptionMaterials");

            migrationBuilder.DropColumn(
                name: "Coefficient",
                schema: "material",
                table: "BuildingMaterials");

            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "material",
                table: "BuildingMaterials");

            migrationBuilder.DropColumn(
                name: "Coefficient",
                schema: "hr",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "hr",
                table: "Workers");
        }
    }
}
